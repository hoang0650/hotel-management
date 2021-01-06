using MyFinance.Core;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using MyFinance.Utils;
using MyFinance.Domain.Enum;
using System.Transactions;
using System.Device.Location;
using System.Data.Entity.Spatial;

namespace MyFinance.Business
{
    public interface IHotelBusiness : IBusinessBase
    {
        bool AddHotel(HotelModel model);
        List<Hotel> GetallBy();
        HotelModel GetById(int hotelId);
        bool CreateHotelFromSystem(HotelModel model);
        bool RegisterHotel(HotelRegisterModel data);
        List<HotelModel> GetHotels(InvoiceFilterModel filter, out int total);
        List<RoomTypeViewModel> GetHotelUtilityBy();
        bool AddTicket(TicketModel model);
        List<TicketModel> ListTicket();
        bool RegisterUser(UserViewModel data);
        IList<OwnerHotelDTO> GetHotelOwnerByUser(int userId);
        UserLoginViewModel ChangeViewHotel(int hotelId);
        IList<HotelModel> GetHotelNearBy(HotelRequestModel filter, out int total);
        bool DeleteHotel(List<int> Ids);
        bool InsertOrUpdateCamera(CameraDTO dto);
        CameraDTO GetCameraByHotel();
        bool ResetDataHotel(int hotelId);

    }
    public class HotelBusiness : BusinessBase, IHotelBusiness
    {
        private readonly MyFinanceContext _context;
        private readonly IUnitOfWork unitOfWork;

        public HotelBusiness(
            IUnitOfWork unitOfWork,
            MyFinanceContext context)
        {
            this.unitOfWork = unitOfWork;
            _context = context;
        }


       
        public List<RoomTypeViewModel> GetHotelUtilityBy()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var featureMap = unitOfWork.Repository<UtilityMapping>().GetMany(a => a.HotelId == hotelId && a.RoomTypeId <= 0);
            var roomtype = unitOfWork.Repository<Utility>().GetMany(a => a.UtilityType == (int)UtilitiesType.Hotel && !a.IsDeleted).ToList();
            var result = AutoMapper.Mapper.Map<List<RoomTypeViewModel>>(roomtype);
            foreach (var item in featureMap)
            {
                var row = result.Where(a => a.Id == item.UtilityId).FirstOrDefault();
                row.IsSelected = true;
            }

            return result;
        }

      
        public bool RegisterHotel(HotelRegisterModel data)
        {
            if(data.NumFloors<=0||data.NumRooms<=0)
            {
                this.AddError("Cần nhập số phòng và số tầng !");
                return false;
            }
            if (string.IsNullOrWhiteSpace(data.Name))
            {
                this.AddError("Bạn chưa nhập tên khách sạn !");
                return false;
            }
            var m_userIsExist = unitOfWork.Repository<User>().Get(a=>a.Email== data.User.Email);
            //if (string.IsNullOrWhiteSpace(data.User.Email) || string.IsNullOrWhiteSpace(data.User.Password))
            //{
            //    this.AddError("Bạn chưa nhập tài khoản quản trị !");
            //    return false;
            //}

            if (m_userIsExist!=null && !data.IsOwner)
            {
                this.AddError("Tài khoản quản lý đã tồn tại !");
                return false;
            }
            using (var transaction = new TransactionScope())
            {
                var hotelRepo = unitOfWork.Repository<Hotel>();
                var row = AutoMapper.Mapper.Map<Hotel>(data);// new Hotel() { Name = model.Name, Email = model.Email, Phone = model.Phone, Website = model.Website };
                row.IsActive = true;
                row.CreatedDate = DateTime.Now;
                hotelRepo.Add(row);
                unitOfWork.Commit();
                var ownerHotel = new OwnerHotel()
                {
                    Hotel = row,
                    
                    IsSelected = true,
                    
                };
                if (m_userIsExist==null)
                {
                    data.User.HotelId = row.Id;
                    data.User.IsOwner = true;
                    data.User.UserType = UserType.Admin;
                    data.User.Password = Utils.CommonUtil.CreateMD5(data.User.Password);

                    IoC.Get<IUserBusiness>().AddUser(data.User);
                    var m_user = unitOfWork.Repository<User>().Get(a => a.HotelId == row.Id);
                    ownerHotel.User = m_user;
                }
                else
                {
                    bool m_IshotelOwner = unitOfWork.Repository<OwnerHotel>()
                        .GetQueryable()
                        .Where(a => a.UserOwnerId == m_userIsExist.Id && a.IsSelected).FirstOrDefault() != null;
                    ownerHotel.IsSelected = false;
                    ownerHotel.UserOwnerId = m_userIsExist.Id;
                }
                
                
              
                unitOfWork.Repository<OwnerHotel>().Add(ownerHotel);
                
                AutoGeneralRoomClass(row.Id);
                AutoGeneralFloorAndRoom(row.Id);
                AutoGeneralConfig(row.Id);
                transaction.Complete();
            }
           

            return !this.HasError;
        }
        public bool CreateHotelFromSystem(HotelModel model)
        {
            var hotelRepo = unitOfWork.Repository<Hotel>();
            var row = AutoMapper.Mapper.Map<Hotel>(model);// new Hotel() { Name = model.Name, Email = model.Email, Phone = model.Phone, Website = model.Website };
            hotelRepo.Add(row);
            unitOfWork.Commit();
            AutoGeneralRoomClass(row.Id);
            AutoGeneralFloorAndRoom(row.Id);           
            AutoGeneralConfig(row.Id);
            AutoGeneralUser(row.Id);
            return !this.HasError;
        }


        private void AutoGeneralFloorAndRoom(int hoteid)
        {
            var hotelRepo = unitOfWork.Repository<Hotel>();
            var floorRepository = unitOfWork.Repository<Floor>();
            var roomRepository = unitOfWork.Repository<Room>();
            var roomclassRepo = unitOfWork.Repository<RoomClass>();
            var hotel = hotelRepo.GetById(hoteid);
            if (hotel != null)
            {
                if (hotel.NumFloors > 0 && hotel.NumRooms > 0)
                {

                    var roomclass=roomclassRepo.GetMany(a => a.HotelId == hoteid).FirstOrDefault();
                    int roomper = hotel.NumRooms / hotel.NumFloors;
                    for (int i = 1; i <= hotel.NumFloors; i++)
                    {
                        var floor = new Floor() { HotelId = hotel.Id, Name = "Tầng " + i };
                        floorRepository.Add(floor);
                        for (int j = 1; j <= roomper; j++)
                        {
                            var room = new Room() { Floor = floor, HotelId = hotel.Id, Name = i + "0" + j, RoomClass = roomclass, RoomStatus = RoomStatus.InActive };
                            roomRepository.Add(room);
                        }
                        if (i == hotel.NumFloors)
                        {
                            int num = hotel.NumRooms - (roomper * hotel.NumFloors);
                            int fl = 1;
                            while (num > 0)
                            {
                                var room = new Room() { Floor = floor, HotelId = hotel.Id, Name = i + "0" + roomper + fl, RoomClass = roomclass,RoomStatus=RoomStatus.InActive };
                                roomRepository.Add(room);
                                num--;
                                fl++;
                            }
                        }
                    }
                    unitOfWork.Commit();
                }

            }
        }

        private void AutoGeneralRoomClass(int hoteid)
        {
            var rclRepo = unitOfWork.Repository<RoomClass>();
            var rclAttRepo = unitOfWork.Repository<RoomAttribute>();
            var roomclass = (from a in _context.RoomClasses
                              where a.Id == 1
                              select a).FirstOrDefault();

            var roomClassNew = new RoomClass() {
                HotelId=hoteid,
                Name ="Phòng đôi",// roomclass.Name,
                NumBed =2,// roomclass.NumBed,
                NumCustomer =4,// roomclass.NumCustomer,
                IsDeleted=false
               
            };
            rclRepo.Add(roomClassNew);

            //var configclass = (from a in _context.ConfigPrice
            //                   where a.RoomClassId == roomclass.Id && a.ConfigType==ConfigType.ByDefault
            //                 select a).FirstOrDefault();

            var configprice = new ConfigPrice() { 
                IsDefault=true,//configclass.IsDefault,
                IsActive=true,//configclass.IsActive,
                Name="Mặc định",//configclass.Name,
                ConfigType=ConfigType.ByDefault,//configclass.ConfigType,
                Price=300000,//configclass.Price,
                PriceByDay= 300000,//configclass.PriceByDay,
                PriceByMonth= 3000000,//configclass.PriceByMonth,
                PriceByNight= 350000,//configclass.PriceByNight,
                RoomClass = roomClassNew
            };
          
            unitOfWork.Repository<ConfigPrice>().Add(configprice);
          
            //var AttriConfig = (from a in _context.RoomExtend
            //                   where a.ConfigPriceId == configclass.Id
            //                   select a).ToList();
            //foreach (var item in AttriConfig)
            //{
            //    var AttributeNew = new RoomAttribute()
            //    {
            //        Additional=item.Additional,
            //        Key=item.Key,
            //        ConfigPrice = configprice,
            //        Value=item.Value
            //    };
            //    rclAttRepo.Add(AttributeNew);
            //}
            unitOfWork.Commit();
        }

        private void AutoGeneralConfig(int hotelid)
        {
            //var model = (from a in _context.HotelConfig
            //             where a.HotelId == 1
            //             select a).FirstOrDefault();
            unitOfWork.Repository<HotelConfig>().Add(new HotelConfig() { 
                EndOverNight=3,//model.EndOverNight,
                HotelId=hotelid,
                StartOverNight=21,//model.StartOverNight,
                TimeCheckOut=12,//model.TimeCheckOut,
                TimeRound=15,//model.TimeRound,
                HasCleaner=true,//model.HasCleaner,
                OverCustomer=2,//model.OverCustomer,
                RoomStatusColor= "Active_#eb5b5b_Đang sử dụng;InActive_#ae83e6_Đang trống;Refresh_#1c0a0a_Chưa dọn dẹp;Booking_#41329c_Đã đặt trước;Repair_#5c2020_Đang sửa chữa",//model.RoomStatusColor,
                TimeCheckIn=12,//model.TimeCheckIn

            });
            unitOfWork.Commit();
        }

        private void AutoGeneralUser(int hotelId)
        {
            var data = new User() {
                Email = "Admin" + hotelId,
                DateCreated=DateTime.Now,
                DateLastPasswordChange=DateTime.Now,
                FullName="Admin",
                HotelId=hotelId,
                IsActive=true,
                IsApproved=true,
                IsOwner=true,
                IsDeteled=false,
                Password=MyFinance.Utils.CommonUtil.CreateMD5("123456")
            };
            unitOfWork.Repository<User>().Add(data);
            unitOfWork.Commit();
        }
        public bool DeleteHotel(List<int> Ids)
        {
            try
            {
                var m_hotelRepository = unitOfWork.Repository<Hotel>();
                if (Ids.Any())
                {
                    foreach (var item in Ids)
                    {
                        var m_hotel = m_hotelRepository.Get(a => a.Id == item);
                        m_hotel.IsDeleted = true;
                        m_hotelRepository.Update(m_hotel);
                    }
                    unitOfWork.Commit();
                }
                return !this.HasError;
            }
            catch (Exception)
            {
                return false;
                
            }
            
        }
        public bool AddHotel(HotelModel model)
        {
            if(model==null)
            { this.AddError("Dữ liệu khách sạn rỗng !"); return false; }
            var hotelRepo = unitOfWork.Repository<Hotel>();
            if (model.Id>0)
            {
                var hotel = hotelRepo.GetById(model.Id);
                hotel.Address = model.Address;
                hotel.Name = model.Name;
                hotel.NumFloors = model.NumFloors;
                hotel.NumRooms = model.NumRooms;
                hotel.DateExpired = model.DateExpired.HasValue?model.DateExpired.Value:hotel.DateExpired;
                hotel.Website = model.Website;
                hotel.Email = model.Email;
                hotel.Phone = model.Phone;
                hotel.Logo = model.Logo;
                hotel.Description = model.Description;
                hotel.Policy = model.Policy;
                hotelRepo.Update(hotel);
                if(model.Images.Any())
                {
               
                    var imageRepo =  unitOfWork.Repository<Gallery>();
                    var images = imageRepo.GetMany(a => a.HotelId == model.Id && !a.RoomTypeId.HasValue).ToList();
                    foreach (var item in images)
                    {
                        imageRepo.Delete(item);
                    }
                    IoC.Get<IGalleryBusiness>().AddImage(model.Images);
                }
                
               var MapFeatureRepo = unitOfWork.Repository<UtilityMapping>();

                var currentMapFeature = MapFeatureRepo.GetMany(a => a.HotelId == model.Id && a.RoomTypeId<=0);
                if (model.HotelUtilityIds != null)
                {
                    if (model.HotelUtilityIds.Any() && currentMapFeature.Any())
                    {
                        foreach (var item in currentMapFeature)
                        {
                            MapFeatureRepo.Delete(item);
                        }
                    }

                    if (model.HotelUtilityIds.Any())
                    {
                        foreach (var item in model.HotelUtilityIds)
                        {
                            var row = new UtilityMapping()
                            {
                                HotelId = WorkContext.BizKasaContext.HotelId,
                                RoomTypeId = 0,
                                UtilityId = item,
                                IsDeleted = false
                            };
                            MapFeatureRepo.Add(row);
                        }

                    }
                }
               

            }
            else
            {
                var row = new Hotel() {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Website =model.Website,
                    Address=model.Address,
                    Longitude=model.Longitude,
                    Latitude=model.Latitude,
                    Source=model.Source,
                    Logo=model.Logo,
                    NextHour=model.NextHour,
                    FirstHour=model.FisrtHour,
                    OverNight=model.OverNight,
                    IsActive=true
                };
                row.CreatedDate = DateTime.Now;
                hotelRepo.Add(row);
            }
           
            unitOfWork.Commit();
            return !this.HasError;
        }

        /// <summary>
        /// Lấy danh sách các khách sạn được quản lý bởi 1 user
        /// </summary>
        /// <param name="userId">Id người quản lý</param>
        /// <returns></returns>
        public IList<OwnerHotelDTO> GetHotelOwnerByUser(int userId)
        {
            List<OwnerHotelDTO> result = new List<OwnerHotelDTO>();
            var m_hoteOwners = unitOfWork.Repository<OwnerHotel>().GetMany(a => a.UserOwnerId == userId).ToList();
            if (m_hoteOwners.Any())
            {
                var m_hotelRepository = unitOfWork.Repository<Hotel>();
                foreach (var item in m_hoteOwners)
                {
                    var m_hotel = m_hotelRepository.Get(a => a.Id == item.HotelId && !a.IsDeleted);
                    if (m_hotel == null) continue;
                    var row = new OwnerHotelDTO() {
                    HotelId= item.HotelId,
                    HotelName= m_hotel!=null? m_hotel.Name:string.Empty,
                    IsSelected=item.IsSelected,
                    DateExpired= m_hotel.DateExpired
                    };
                    result.Add(row);
                }
              
            }
            return result;
        }


        /// <summary>
        /// Thay đổi khách sạn cần xem
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public UserLoginViewModel ChangeViewHotel(int hotelId)
        {
            var m_hotelownerRepository = unitOfWork.Repository<OwnerHotel>();
            var m_hoteOwners = m_hotelownerRepository.GetMany(a => a.UserOwnerId == WorkContext.BizKasaContext.UserId).ToList();
            foreach (var item in m_hoteOwners)
            {
                if (item.HotelId == hotelId)
                    item.IsSelected = true;
                else
                    item.IsSelected = false;
                m_hotelownerRepository.Update(item);
            }
            unitOfWork.Commit();
            string m_email = WorkContext.BizKasaContext.UserName;

          return   IoC.Get<IUserBusiness>().Relogin(m_email);
        }

        public List<Hotel> GetallBy()
        {
         var result=   unitOfWork.Repository<Hotel>().GetAll();
         return result.ToList();
        }
        public List<HotelModel> GetHotels(InvoiceFilterModel filter, out int total)
        {
           
            List<HotelModel> result = new List<HotelModel>();
            filter.FromDate = filter.FromDate.HasValue ? filter.FromDate.Value.ToMinDate() : filter.FromDate;
            filter.ToDate = filter.ToDate.HasValue ? filter.ToDate.Value.ToMaxDate() : filter.ToDate;
            var m_hotelRepository = unitOfWork.Repository<Hotel>().GetQueryable();
            m_hotelRepository = m_hotelRepository.Where(a => !a.IsDeleted);
            if (filter.Source.HasValue)
                m_hotelRepository = m_hotelRepository.Where(a => a.Source == filter.Source);
            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                m_hotelRepository = m_hotelRepository.Where(a => a.CreatedDate >= filter.FromDate.Value && a.CreatedDate <= filter.ToDate.Value);
            if (!string.IsNullOrWhiteSpace(filter.Address))
                m_hotelRepository = m_hotelRepository.Where(a => a.Address.Contains(filter.Address));
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                m_hotelRepository = m_hotelRepository.Where(a => 
                a.Name.Contains(filter.Keyword)
                || a.Address.Contains(filter.Keyword) 
                || a.Phone.Contains(filter.Keyword) 
                || a.Website.Contains(filter.Keyword));

            total = m_hotelRepository.Count();
            var m_hotels = m_hotelRepository
                .Select(a=>new HotelModel() {
                    Address = a.Address,
                    Name = a.Name,
                    Id = a.Id,
                    NumFloors = a.NumFloors,
                    NumRooms = a.NumRooms,
                    CreateDate = a.CreatedDate,
                    Phone = a.Phone,
                    FisrtHour = a.FirstHour,
                    NextHour = a.NextHour,
                    OverNight = a.OverNight,
                    Description = a.Description,
                    Logo = a.Logo,
                    DateExpired = a.DateExpired
                })
                .OrderByDescending(a => a.CreateDate).Skip((filter.Page.currentPage-1) * filter.Page.pageSize).Take(filter.Page.pageSize).ToList();
           

            return m_hotels;
        }

        /// <summary>
        /// tìm khách sạn quanh đây
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<HotelModel> GetHotelNearBy(HotelRequestModel filter, out int total)
        {
           
            var m_hotelRepository = unitOfWork.Repository<Hotel>().GetQueryable();
           // var coord = new GeoCoordinate(filter.Latitude, filter.Longitude);
            DbGeography searchLocation = DbGeography.FromText(String.Format("POINT({0} {1})", filter.Longitude, filter.Latitude));

            total = m_hotelRepository.Select(a => new HotelModel()
            {
                Distance = searchLocation.Distance(DbGeography.FromText("POINT(" + a.Longitude + " " + a.Latitude + ")")).Value,
                Address = a.Address,
                Name = a.Name,
                Id = a.Id,
                NumFloors = a.NumFloors,
                NumRooms = a.NumRooms,
                CreateDate = a.CreatedDate,
                Phone = a.Phone,
                FisrtHour = a.FirstHour,
                NextHour = a.NextHour,
                OverNight = a.OverNight,
                Description = a.Description,
                Logo = a.Logo
            }).Where(a => a.Distance <= 5000).Count();



            var m_hotels = m_hotelRepository.Select(a=>new HotelModel() {
                Distance= searchLocation.Distance(DbGeography.FromText("POINT(" + a.Longitude + " " + a.Latitude + ")")).Value,
                Address = a.Address,
                Name = a.Name,
                Id = a.Id,
                NumFloors = a.NumFloors,
                NumRooms = a.NumRooms,
                CreateDate = a.CreatedDate,
                Phone = a.Phone,
                FisrtHour = a.FirstHour,
                NextHour = a.NextHour,
                OverNight = a.OverNight,
                Description = a.Description,
                Logo = a.Logo
            }).Where(a=>a.Distance<=5000)
                .OrderBy(a => a.Distance)
                .Skip((filter.Page.currentPage - 1) * filter.Page.pageSize)
                .Take(filter.Page.pageSize)
                .ToList();
            return m_hotels;
        }

        public HotelModel GetById(int hotelId)
        {
            var HotelRepository = unitOfWork.Repository<Hotel>();
            var result = HotelRepository.GetQueryable()
                                        .Where(a=>a.Id== hotelId)
                                        .Select(b=> new HotelModel() {
                                            Name=b.Name,
                                            NumFloors=b.NumFloors,
                                            NumRooms=b.NumRooms,
                                            DateExpired=b.DateExpired,
                                            Id=b.Id
                                        }).FirstOrDefault();

            //var map = AutoMapper.Mapper.Map<HotelModel>(result);

            //var images =  unitOfWork.Repository<Gallery>().GetMany(a => a.HotelId == hotelId&&!a.RoomTypeId.HasValue).ToList();
            //var mapImages = AutoMapper.Mapper.Map<List<GalleryModel>>(images);
            //map.Images = mapImages;

            return result;
        }

        #region Camera
        public bool InsertOrUpdateCamera(CameraDTO dto)
        {
            var m_cameraRepository = unitOfWork.Repository<Camera>();
            try
            {
                if (dto.Id > 0)
                {
                    //update
                    var m_camera = m_cameraRepository.Get(a => a.Id == dto.Id);
                    m_camera.IsDefault = dto.IsDefault;
                    m_camera.Link = dto.Link;
                    m_camera.NumCamera = dto.NumCamera;
                    m_camera.Password = dto.Password;
                    m_camera.Username = dto.Username;
                    m_camera.CameraDefault = dto.CameraDefault;
                    m_cameraRepository.Update(m_camera);
                }
                else
                {
                    // Add new
                    var row = new Camera() {
                        HotelId=WorkContext.BizKasaContext.HotelId,
                        IsDefault=dto.IsDefault,
                        Link=dto.Link,
                        NumCamera=dto.NumCamera,
                        Password=dto.Password,
                        Username=dto.Username,
                        CameraDefault=dto.CameraDefault
                    };
                    m_cameraRepository.Add(row);
                }
                unitOfWork.Commit();
                return !this.HasError;
            }
            catch (Exception)
            {

                return false;
            }
            

        }

        public CameraDTO GetCameraByHotel()
        {
         return   unitOfWork.Repository<Camera>().GetQueryable()
                .Where(a=>a.HotelId==WorkContext.BizKasaContext.HotelId)
                .Select(a=>new CameraDTO() {
                    HotelId=a.HotelId,
                    Id=a.Id,
                    IsDefault=a.IsDefault,
                    Link=a.Link,
                    NumCamera=a.NumCamera,
                    Username=a.Username,
                    Password=a.Password,
                    CameraDefault=a.CameraDefault
                }).FirstOrDefault();
        }
        #endregion

        #region Reset data hotel
        /// <summary>
        /// reset data hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public bool ResetDataHotel(int hotelId)
        {
            try
            {
                // init repository
                var m_orderRepository = unitOfWork.Repository<Order>();
                var m_orderdetailRepository = unitOfWork.Repository<OrderDetail>();
                var m_roomRepository = unitOfWork.Repository<Room>();
                var m_orderServiceRepository = unitOfWork.Repository<OrderService>();
                var m_orderCustomerRepository = unitOfWork.Repository<OrderCustomer>();
                var m_invoiceDetailRepository = unitOfWork.Repository<InvoiceDetail>();
                var m_invoiceRepository = unitOfWork.Repository<Invoice>();
                var m_customerRepository = unitOfWork.Repository<Customer>();

                var m_shiftRepository = unitOfWork.Repository<Shift>();

                var m_historyRepository = unitOfWork.Repository<History>();
                // get orderId 
                var m_orderIds = m_orderRepository
                    .GetQueryable()
                    .Where(a => a.HotelId == hotelId)
                    .Select(a => a.Id)
                    .ToList();
                // delete invoice detail
                m_invoiceDetailRepository.DeleteRange(a => a.HotelId == hotelId);
                unitOfWork.Commit();
                // delete invoice
                m_invoiceRepository.DeleteRange(a => a.HotelId == hotelId);
                unitOfWork.Commit();
                
                
                // delete order service 
                m_orderServiceRepository.DeleteRange(a =>  m_orderIds.Contains(a.OrderId));
                unitOfWork.Commit();
                // delete order customer
                m_orderCustomerRepository.DeleteRange(a => m_orderIds.Contains(a.OrderId));
                unitOfWork.Commit();
                // delete order detail
                m_orderdetailRepository.DeleteRange(a => m_orderIds.Contains(a.OrderId));
                unitOfWork.Commit();

                // delete order 
                m_orderRepository.DeleteRange(a => a.HotelId == hotelId);
                unitOfWork.Commit();

                // tra lai trang thai phong " dang trong"
                var m_rooms = m_roomRepository.GetQueryable().Where(a => a.HotelId == hotelId && (a.RoomStatus == RoomStatus.Active|| a.RoomStatus == RoomStatus.Refresh)).ToList();
                foreach (var item in m_rooms)
                {
                    item.RoomStatus = RoomStatus.InActive;
                    m_roomRepository.Update(item);
                }
                // delete customer
                m_customerRepository.DeleteRange(a => a.HotelId == hotelId);
                unitOfWork.Commit();
                // delete shift
                //m_shiftRepository.DeleteRange(a => a.HotelId == hotelId);
                //unitOfWork.Commit();
                // delete histories 
                m_historyRepository.DeleteRange(a => a.HotelId == hotelId);

                unitOfWork.Commit();

                return !this.HasError;
            }
            catch (Exception ex)
            {
                base.AddError(ex.Message);
                return false;
               
            }
           




        }

        #endregion

        #region Ticket



        public bool AddTicket(TicketModel model)
        {
            try
            {
                var map = AutoMapper.Mapper.Map<Ticket>(model);
                map.UserId = WorkContext.BizKasaContext.UserId;
                unitOfWork.Repository<Ticket>().Add(map);
                unitOfWork.Commit();
                return !this.HasError;
            }
            catch (Exception)
            {
                return false;
            }
           

        }

        public List<TicketModel> ListTicket()
        {
            
            var tickets = unitOfWork.Repository<Ticket>().GetMany(a=>a.UserId==WorkContext.BizKasaContext.UserId).ToList();
            return AutoMapper.Mapper.Map<List<TicketModel>>(tickets);
        }

        public bool RegisterUser(UserViewModel data)
        {
            try
            {
                if (IoC.Get<IUserBusiness>().CheckUserExist(data.Email))
                {
                    this.AddError("Tài khoản quản lý đã tồn tại !");
                    return false;
                }
                data.Password = Utils.CommonUtil.CreateMD5(data.Password);
                IoC.Get<IUserBusiness>().AddUser(data);

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                base.AddError(ex.Message);
                return false;
                
            }
            

            return !base.HasError;
        }

        #endregion
    }
}
