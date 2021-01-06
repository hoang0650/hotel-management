using System;
using System.Collections.Generic;
using System.Linq;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;

namespace MyFinance.Business
{
    public interface IRoomBusiness : IBusinessBase
    {
        List<RoomClassModel> AddRoomClass(RoomClassModel data);
        List<RoomClassModel> GetRoomClassBy(int hotelId);
        void AutoGeneralFloorAndRoom(int hoteid);
        List<FloorRowModel> GetRoomsByFloor(int hotelId);
        List<ConfigPriceRowModel> GetConfigPriceByRoom(int roomid);
        List<RoomsClassRowModel> GetRoomForCheckinMutil();

        RoomStaticModel GetRoomsStaticByTime(int hotelId, RoomSearchByTimeModel model);
        RoomForEditModel GetRoomForEdit(int roomId);

        List<FloorModel> GetListFloor();
        List<RoomClassRow> GetListRoomClass();
        bool EditRoom(RoomForEditModel model);
        bool DeleteRoom(int roomId);
        bool InsertOrUpdateFloor(FloorModel model);
        FloorModel GetFloorBy(int floorId);
        RoomClassModel GetRoomClassById(int roomtypeId);
        List<RoomTypeViewModel> GetRoomUtilityBy(int roomTypeId);
        List<FloorRowModel> GetRoomsByClass();
        List<RoomForEditModel> GetListRooms();
        bool AddRoom(List<RoomForEditModel> data);
        string GetStaticRoom();
        List<FloorRowModel> GetRoomsByStatus(RoomStatus status);
        bool ChangeStatusRoom(int roomId, RoomStatus status);
        List<RoomClassModel> RequestAddOrUpdateConfigPriceForOne(ConfigPriceViewModel data,int roomClassId);
        List<RoomsClassRowModel> GetRoomAvailable(DateTime? fromDate, DateTime? toDate);
        List<ConfigPriceRowModel> GetConfigPriceBy(int roomid);
        bool RefreshRoom(int roomId);
        bool DeleteFloor(List<int> ids);
        bool DeleteRoomClass(List<int> ids);
        bool DeleteConfigPrice(List<int> ids);
        List<RoomModel> GetRoomsByShort();
    }

    public class RoomBusiness : BusinessBase, IRoomBusiness
    {
        private readonly MyFinanceContext _context;
        private readonly IUnitOfWork unitOfWork;
        public RoomBusiness(
             IUnitOfWork unitOfWork
           
            , MyFinanceContext context)
        {
           
            this.unitOfWork = unitOfWork;
            this._context = context;
        }

        #region RoomClass

        public bool DeleteRoomClass(List<int> ids)
        {
            if (!ids.Any())
            {
                this.AddError("Dữ liệu xóa tầng rỗng !");
                return false;
            }
            var repo = unitOfWork.Repository<RoomClass>();
            int m_rooms = unitOfWork.Repository<Room>().GetQueryable()
                .Count(a => ids.Contains(a.RoomClassId) && !a.IsDeleted);
            if (m_rooms > 0)
            {
                this.AddError("Cần xóa phòng liên quan trước khi xóa loại phòng !");
                return false;
            }
            foreach (var item in ids)
            {
                var roomclass = repo.GetById(item);
                roomclass.IsDeleted = true;
                repo.Update(roomclass);

            }
            unitOfWork.Commit();

            return !this.HasError;
        }

        public bool DeleteConfigPrice(List<int> ids)
        {
            if (!ids.Any())
            {
                this.AddError("Chưa chọn cấu hình cần xóa !");
                return false;
            }
            var repo = unitOfWork.Repository<ConfigPrice>();
            foreach (var item in ids)
            {
                var conf = repo.GetById(item);
                conf.IsDeleted = true;
                repo.Update(conf);

            }
            unitOfWork.Commit();

            return !this.HasError;
        }


        #endregion
        public bool RefreshRoom(int roomId)
        {
            var roomRepo = unitOfWork.Repository<Room>();
            var room = roomRepo.GetMany(a => a.Id == roomId && !a.IsDeleted).FirstOrDefault();
            if (room == null)
            {
                this.AddError("Không tìm thấy phòng cần dọn !");
                return false;
            }
            room.RoomStatus = RoomStatus.InActive;
            roomRepo.Update(room);
            unitOfWork.Commit();
            return !this.HasError;
        }

        public List<RoomsClassRowModel> GetRoomAvailable(DateTime? fromDate, DateTime? toDate)
        {
            fromDate = fromDate.HasValue ? fromDate.Value.ToMinDate() : DateTime.Now.ToMinDate();
            toDate = toDate.HasValue ? toDate.Value.ToMaxDate() : DateTime.Now.ToMaxDate();

            int hotelId = WorkContext.BizKasaContext.HotelId;
            List<RoomsClassRowModel> result = new List<RoomsClassRowModel>();
            var roomIds = unitOfWork.Repository<Order>().GetQueryable()
                .Where(a => a.HotelId == hotelId
                           && ((a.CheckInDate >= fromDate.Value && a.CheckInDate <= toDate.Value)
                          /* || (a.CheckInDate <= fromDate.Value && a.CheckOutDate > fromDate.Value)*/)
                           //  && a.CheckInDate <= fromDate.Value && a.CheckOutDate >= fromDate.Value
                           && (a.OrderStatus == (int)OrderStatus.Booking || a.OrderStatus == (int)OrderStatus.CheckIn))
                .Select(a => a.RoomId).ToList();

            var room = unitOfWork.Repository<RoomClass>().GetQueryable()
                .Where(a => a.HotelId == hotelId && !a.IsDeleted)
                .Select(a => new RoomsClassRowModel
                {
                    Id = a.Id,
                    // ConfigPriceSelected = 0,
                    // Price =a.ConfigPrices.Where(x => x.IsDefault).Select(y=>y.Price).FirstOrDefault(),
                    Name = a.Name,
                    //RoomTotal =a.Rooms.Count(),

                    //RoomUsed =a.Rooms.Where(x=> roomIds.Contains(x.Id)).Count() ,

                    listConfig = a.ConfigPrices.Where(x => !x.IsDeleted && x.IsActive).Select(e => new ConfigPriceRowModel()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        PriceByDay = e.PriceByDay,
                        PriceByNight = e.PriceByNight
                    }).ToList(),

                    Rooms = a.Rooms.Where(r => !r.IsDeleted && !roomIds.Contains(r.Id)).Select(x => new RoomsViewAvailableModel { Id = x.Id, Name = x.Name }).ToList(),

                    // RoomAvailable = a.Rooms.Select(r=> new RoomsViewAvailableModel { Id = r.Id, Name = r.Name }).ToList().Count(),
                }).ToList();

            result = room;
            return result;
        }

        public bool ChangeStatusRoom(int roomId, RoomStatus status)
        {
            var roomRepo = unitOfWork.Repository<Room>();
            var room = roomRepo.GetMany(a => a.Id == roomId && !a.IsDeleted).FirstOrDefault();
            if (room == null)
            {
                this.AddError("Không tìm thấy phòng cần xóa !");
                return false;
            }
            room.RoomStatus = status;
            roomRepo.Update(room);
            unitOfWork.Commit();
            return !this.HasError;
        }

        public string GetStaticRoom()
        {
            int hotelid = WorkContext.BizKasaContext.HotelId;
            var config = unitOfWork.Repository<HotelConfig>().GetMany(a => a.HotelId == hotelid).Select(a => a.RoomStatusColor).FirstOrDefault();
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(config))
            {
                var color = config.Split(';');
                if (color.Length > 0)
                {
                    for (int i = 0; i < color.Length; i++)
                    {
                        var row = color[i].Split('_');
                        RoomStatus status = (RoomStatus)Enum.Parse(typeof(RoomStatus), row[0]);
                        if(status!= RoomStatus.Booking && status != RoomStatus.Repair)
                        {
                            var count = unitOfWork.Repository<Room>().GetQueryable()
                                                       .Where(a => a.HotelId == hotelid && a.RoomStatus == status && !a.IsDeleted).Count();
                            color[i] = color[i] + '_' + count;
                            if (string.IsNullOrEmpty(result))
                            {
                                result = color[i];
                            }
                            else
                            {
                                result += ';' + color[i];
                            }
                        }
                       
                    }
                }
            }
            return result;
        }

        #region Floor
        public bool DeleteFloor(List<int> ids)
        {
            if (!ids.Any())
            {
                this.AddError("Dữ liệu xóa tầng rỗng !");
                return false;
            }
            var repo = unitOfWork.Repository<Floor>();
            foreach (var item in ids)
            {
                var floor = repo.GetById(item);
                floor.IsDeleted = true;
                repo.Update(floor);
               
            }
            unitOfWork.Commit();

            return !this.HasError;
        }
        #endregion
        public bool AddRoom(List<RoomForEditModel> data)
        {
            if (!data.Any())
            {
                this.AddError("Dữ liệu thêm phòng rỗng !");
                return false;
            }
            foreach (var item in data)
            {
                var row = new Room()
                {
                    FloorId = item.FloorId,
                    HotelId = WorkContext.BizKasaContext.HotelId,
                    Name = item.RoomName,
                    RoomClassId = item.RoomClassId,
                    RoomStatus = RoomStatus.InActive
                };
                unitOfWork.Repository<Room>().Add(row);
            }
            unitOfWork.Commit();

            return !this.HasError;
        }
        public List<RoomForEditModel> GetListRooms()
        {
            var rooms = unitOfWork.Repository<Room>().GetMany(a => a.HotelId == WorkContext.BizKasaContext.HotelId && !a.IsDeleted).OrderBy(a=>a.RoomClassId).ToList();
            var result = AutoMapper.Mapper.Map<List<RoomForEditModel>>(rooms);
            return result;
        }
        public FloorModel GetFloorBy(int floorId)
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;

            var floors = unitOfWork.Repository<Floor>().GetMany(a => a.Id == floorId && !a.IsDeleted).FirstOrDefault();
            var result = AutoMapper.Mapper.Map<FloorModel>(floors);
            return result;
        }

        public bool InsertOrUpdateFloor(FloorModel model)
        {
            var floorRepo = unitOfWork.Repository<Floor>();
            var roomRepo = unitOfWork.Repository<Room>();
            var hotelRepo = unitOfWork.Repository<Hotel>();
            int hotelid = WorkContext.BizKasaContext.HotelId;
            if (model.Id > 0)
            {
                var floor = floorRepo.GetMany(a => a.Id == model.Id && !a.IsDeleted).FirstOrDefault();
                floor.Name = model.Name;
                floorRepo.Update(floor);
                unitOfWork.Commit();
            }
            else
            {
                var row = new Floor()
                {
                    HotelId = hotelid,
                    IsDeleted = false,
                    Name = model.Name
                };
                floorRepo.Add(row);

                var hotel = hotelRepo.GetById(hotelid);
                hotel.NumFloors += 1;
                hotelRepo.Update(hotel);

                if (model.NumRooms > 0 && model.RoomClassId > 0)
                {

                    int CurrentFloor = hotel.NumFloors;
                    for (int i = 1; i <= model.NumRooms; i++)
                    {
                        var room = new Room()
                        {
                            Name = i < 10 ? CurrentFloor + "0" + i.ToString() : CurrentFloor + i.ToString(),
                            HotelId = hotelid,
                            RoomStatus = RoomStatus.InActive,
                            Floor = row,
                            IsDeleted = false,
                            RoomClassId = model.RoomClassId
                        };
                        roomRepo.Add(room);
                    }
                }
            }

            unitOfWork.Commit();

            return !this.HasError;
        }

        public bool DeleteRoom(int roomId)
        {
            var roomRepo = unitOfWork.Repository<Room>();
            var room = roomRepo.GetMany(a => a.Id == roomId && !a.IsDeleted).FirstOrDefault();
            if (room == null)
            {
                this.AddError("Không tìm thấy phòng cần xóa !");
                return false;
            }
            int m_numOrderExist = unitOfWork.Repository<Order>()
                .GetQueryable()
                .Count(a=>a.RoomId== roomId && a.OrderStatus==(int)OrderStatus.CheckIn);
            if (m_numOrderExist > 0)
            {
                this.AddError("Phòng này đang được sử dụng cần trả phòng trước khi xóa !");
                return false;
            }
            room.IsDeleted = true;
            roomRepo.Update(room);
            unitOfWork.Commit();
            return !this.HasError;
        }
        /// <summary>
        /// Tra ve danh sach loai phong
        /// </summary>
        /// <returns></returns>
        public List<RoomClassRow> GetListRoomClass()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;

            var floors = unitOfWork.Repository<RoomClass>().GetMany(a => a.HotelId == hotelId && !a.IsDeleted).ToList();
            var result = AutoMapper.Mapper.Map<List<RoomClassRow>>(floors);
            return result;
        }
        public List<FloorModel> GetListFloor()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;

            var floors = (from a in _context.Floors
                          where a.HotelId == hotelId && !a.IsDeleted
                          select new FloorModel
                          {
                              Id = a.Id,
                              Name = a.Name,
                              NumRooms = _context.Rooms.Count(v => v.FloorId == a.Id),
                          }).ToList();

            return floors;
        }

        /// <summary>
        /// Them loai phong cung voi cac cau hinh cua loai phong do
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<RoomClassModel> AddRoomClass(RoomClassModel data)
        {
            int hotelid = WorkContext.BizKasaContext.HotelId;
            var roomclassRepository = unitOfWork.Repository<RoomClass>();
            List<RoomClassModel> result = new List<RoomClassModel>();
            if (data.RoomClass.Id > 0)
            {
                var roomcurrent = roomclassRepository.GetById(data.RoomClass.Id);
                roomcurrent.Name = data.RoomClass.Name;
                roomcurrent.NumCustomer = data.RoomClass.NumCustomer;
                roomclassRepository.Update(roomcurrent);


                var MapFeatureRepo = unitOfWork.Repository<UtilityMapping>();
                var currentMapFeature = MapFeatureRepo.GetMany(a => a.RoomTypeId == data.RoomClass.Id);
                if (data.RoomTypeFeatureIds.Any() && currentMapFeature.Any())
                {
                    foreach (var item in currentMapFeature)
                    {
                        MapFeatureRepo.Delete(item);
                    }
                }

                if (data.Images.Any())
                {
                    var galleryRepo = unitOfWork.Repository<Gallery>();
                    var RoomtypeImages = galleryRepo.GetMany(a => a.RoomTypeId == data.RoomClass.Id).ToList();
                    if (RoomtypeImages.Any())
                    {
                        foreach (var item in RoomtypeImages)
                        {
                            galleryRepo.Delete(item);
                        }
                    }

                    IoC.Get<IGalleryBusiness>().AddImage(data.Images);

                }


                if (data.RoomTypeFeatureIds.Any())
                {
                    foreach (var item in data.RoomTypeFeatureIds)
                    {
                        var row = new UtilityMapping()
                        {
                            HotelId = hotelid,
                            RoomTypeId = data.RoomClass.Id,
                            UtilityId = item,
                            IsDeleted = false
                        };
                        MapFeatureRepo.Add(row);
                    }

                }
              //  AddOrUpdateConfigPriceForList(data, null);
            }
            else
            {
                var model = new RoomClass()
                {
                    Name = data.RoomClass.Name,
                    NumBed = data.RoomClass.NumBed,
                    NumCustomer = data.RoomClass.NumCustomer,
                    HotelId = hotelid
                };
                roomclassRepository.Add(model);


                AddOrUpdateConfigPriceForList(data, model);




            }

            unitOfWork.Commit();
            result = GetRoomClassBy(hotelid);

            return result;
        }


        /// <summary>
        /// Them hoac cap nhat danh sach cau hinh cua mot loai phong
        /// </summary>
        /// <param name="data"></param>
        private void AddOrUpdateConfigPriceForList(RoomClassModel data, RoomClass rl)
        {
            if (data.ConfigPrices.Any())
            {
                foreach (var item in data.ConfigPrices)
                {
                    AddOrUpdateConfigPriceForOne(item, rl);

                }
            }




        }
        /// <summary>
        /// them moi hoac cap nhat thuoc tinh cua cau hinh gia
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <param name="config"></param>
        private void AddOrUpdateAttributeConfigPrice(List<RoomAttributeViewModel> data,Additional type, ConfigPrice config)
        {
            var m_repository = unitOfWork.Repository<RoomAttribute>();

            if (data.Any())
            {
                foreach (var row in data)
                {
                    if (config.Id > 0)
                    {
                        var att = new RoomAttribute()
                        {
                            Additional = type,
                            Key = row.Key,
                            Value = row.Value,
                            ConfigPriceId = config.Id
                        };
                        m_repository.Add(att);

                    }
                    else
                    {
                        var att = new RoomAttribute()
                        {
                            Additional = type,
                            Key = row.Key,
                            Value = row.Value,
                            ConfigPrice = config
                        };
                        m_repository.Add(att);
                    }
                  
                }

            }
        }


        /// <summary>
        /// Them hoac cap nhat mot cau hinh cua mot loai phong
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rl"></param>
        private void AddOrUpdateConfigPriceForOne(ConfigPriceViewModel data, RoomClass rl)
        {
            var roomAttributeRepository = unitOfWork.Repository<RoomAttribute>();
           var repoConfig=unitOfWork.Repository<ConfigPrice>();
                    if (data.ConfigPriceRow.Id > 0)
                    {
                        var configCurrent = repoConfig.GetById(data.ConfigPriceRow.Id);
                        configCurrent.ConfigType = data.ConfigType;
                        configCurrent.Name = data.ConfigPriceRow.Name;
                        configCurrent.PriceByDay = data.ConfigPriceRow.PriceByDay;
                        configCurrent.PriceByMonth = data.ConfigPriceRow.PriceByMonth;
                        configCurrent.PriceByNight = data.ConfigPriceRow.PriceByNight;
                        configCurrent.IsActive = data.ConfigPriceRow.IsActive;

                        if (data.ConfigPriceRow.IsDefault)
                        {
                            if (rl.Id > 0)
                            {
                                var config = unitOfWork.Repository<ConfigPrice>().GetMany(a => a.RoomClassId == rl.Id).ToList();
                                if (config.Any())
                                {
                                    foreach (var item in config)
                                    {
                                        item.IsDefault = false;
                                        repoConfig.Update(item);
                                    }
                                }

                                configCurrent.IsDefault = data.ConfigPriceRow.IsDefault;
                            }
                            else
                                configCurrent.IsDefault = data.ConfigPriceRow.IsDefault;

                        }

                        repoConfig.Update(configCurrent);
                        var AttConficurrent = roomAttributeRepository.GetMany(a => a.ConfigPriceId == data.ConfigPriceRow.Id).ToList();
                        if (AttConficurrent.Any())
                        {
                            foreach (var att in AttConficurrent)
                            {
                                roomAttributeRepository.Delete(att);
                            }
                        }

                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.CheckinDayList, Additional.CheckinByDay, new ConfigPrice() { Id = data.ConfigPriceRow.Id });
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.CheckinNightList, Additional.CheckinBynight, new ConfigPrice() { Id = data.ConfigPriceRow.Id });
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.CheckoutDayList, Additional.CheckoutByDay, new ConfigPrice() { Id = data.ConfigPriceRow.Id });
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.CheckoutNightList, Additional.CheckoutBynight, new ConfigPrice() { Id = data.ConfigPriceRow.Id });
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.AddtionCustomerList, Additional.CustomerOver, new ConfigPrice() { Id = data.ConfigPriceRow.Id });
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.PriceByDayList, Additional.PriceByHour, new ConfigPrice() { Id = data.ConfigPriceRow.Id });
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.ConfigTime, Additional.PriceByDay, new ConfigPrice() { Id = data.ConfigPriceRow.Id });




                    }
                    else
                    {
                        var config = AutoMapper.Mapper.Map<ConfigPrice>(data.ConfigPriceRow);

                        config.ConfigType = data.ConfigType;
                        
                        config.RoomClass = rl;
                        unitOfWork.Repository<ConfigPrice>().Add(config);

                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.CheckinDayList, Additional.CheckinByDay, config);
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.CheckinNightList, Additional.CheckinBynight, config);
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.CheckoutDayList, Additional.CheckoutByDay, config);
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.CheckoutNightList, Additional.CheckoutBynight, config);
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.AddtionCustomerList, Additional.CustomerOver, config);
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.PriceByDayList, Additional.PriceByHour, config);
                        AddOrUpdateAttributeConfigPrice(data.ConfigPriceRow.ConfigTime, Additional.PriceByDay, config);

                    }



                    unitOfWork.Commit();


        }
        
        
        /// <summary>
        /// Them hoac cap nhat mot cau hinh cua mot loai phong cho client request
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public List<RoomClassModel> RequestAddOrUpdateConfigPriceForOne(ConfigPriceViewModel data, int roomClassId)
        {
            var roomclassRepository = unitOfWork.Repository<RoomClass>();
            int hotelid = WorkContext.BizKasaContext.HotelId;
            if (data != null)
            {
                var roomcurrent = roomclassRepository.GetById(roomClassId);
                AddOrUpdateConfigPriceForOne(data, roomcurrent);
            }
            return GetRoomClassBy(hotelid);
        }


        /// <summary>
        /// Tra ve mot loai phong cua mot khach san cu the
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public List<RoomClassModel> GetRoomClassBy(int hotelId)
        {
            List<RoomClassModel> result = new List<RoomClassModel>();
            var Listroomclass = unitOfWork.Repository<RoomClass>().GetMany(a=>a.HotelId== hotelId && !a.IsDeleted).ToList();
            if (Listroomclass.Any())
            {
                foreach (var item in Listroomclass)
                {
                    var rclassModel = GetRoomClassById(item.Id);
                    result.Add(rclassModel);

                }

            }
            return result;
        }


        /// <summary>
        /// Lay danh sach cau hinh gia theo phong
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public List<ConfigPriceRowModel> GetConfigPriceBy(int roomid)
        {
            var roomClass = (from a in _context.Rooms
                             join b in _context.RoomClasses on a.RoomClassId equals b.Id
                             join c in _context.ConfigPrice on b.Id equals c.RoomClassId
                             where a.Id == roomid && !a.IsDeleted && c.IsActive && !c.IsDeleted
                             select new ConfigPriceRowModel()
                             {
                                 IsActive = c.IsActive,
                                 Id = c.Id,
                                 IsDefault = c.IsDefault,
                                 Name = c.Name,
                                 PriceByDay = c.PriceByDay,
                                 RoomClassId = c.RoomClassId
                             }).ToList();
            return roomClass;
        }



        /// <summary>
        /// Tra ve thong tin cua mot loai phong cu the dua vao Id loai phong
        /// </summary>
        /// <param name="roomclassId"></param>
        /// <returns></returns>
        public RoomClassModel GetRoomClassById(int roomclassId)
        {
            RoomClassModel result = new RoomClassModel();
            try
            {
                var roomType = unitOfWork.Repository<RoomClass>().GetById(roomclassId);
                var rclassModel = new RoomClassModel();
                var roomclass = new RoomClassRow()
                {
                    Name = roomType.Name,
                    HotelId = roomType.HotelId,
                    NumBed = roomType.NumBed,
                    Id = roomType.Id,
                    NumCustomer = roomType.NumCustomer

                };
                rclassModel.RoomClass = roomclass;

                // images
                var images = unitOfWork.Repository<Gallery>().GetMany(a => a.RoomTypeId == roomclassId).ToList();
                var mapImages = AutoMapper.Mapper.Map<List<GalleryModel>>(images);
                rclassModel.Images = mapImages;


                //end
                var roomAttributeRepository = unitOfWork.Repository<RoomAttribute>();
                var configs = unitOfWork.Repository<ConfigPrice>().GetMany(a => a.RoomClassId == roomType.Id && !a.IsDeleted).ToList();
                if (configs.Any())
                {
                    foreach (var row in configs)
                    {
                        var configRow = new ConfigPriceRowModel()
                        {
                            IsActive=row.IsActive,
                            IsDefault=row.IsDefault,
                            Name = row.Name,
                            Id = row.Id,
                            PriceByDay = row.PriceByDay,
                            PriceByMonth = row.PriceByMonth,
                            PriceByNight = row.PriceByNight
                        };

                        var attribute = roomAttributeRepository.GetMany(a => a.ConfigPriceId == row.Id).ToList();
                        if (attribute.Any())
                        {
                            List<RoomAttributeViewModel> CheckoutDayList = new List<RoomAttributeViewModel>();
                            List<RoomAttributeViewModel> CheckoutNightList = new List<RoomAttributeViewModel>();
                            List<RoomAttributeViewModel> CheckinDayList = new List<RoomAttributeViewModel>();
                            List<RoomAttributeViewModel> CheckinNightList = new List<RoomAttributeViewModel>();
                            List<RoomAttributeViewModel> AddtionCustomerList = new List<RoomAttributeViewModel>();
                            List<RoomAttributeViewModel> PriceByDayList = new List<RoomAttributeViewModel>();
                            List<RoomAttributeViewModel> ConfigTime = new List<RoomAttributeViewModel>();
                            foreach (var conf in attribute)
                            {
                                switch (conf.Additional)
                                {
                                    case Additional.CheckoutByDay:
                                        var row1 = AutoMapper.Mapper.Map<RoomAttributeViewModel>(conf);
                                        CheckoutDayList.Add(row1);
                                        break;
                                    case Additional.CheckoutBynight:
                                        var row2 = AutoMapper.Mapper.Map<RoomAttributeViewModel>(conf);
                                        CheckoutNightList.Add(row2);
                                        break;
                                    case Additional.CheckinByDay:
                                        var row3 = AutoMapper.Mapper.Map<RoomAttributeViewModel>(conf);
                                        CheckinDayList.Add(row3);
                                        break;
                                    case Additional.CheckinBynight:
                                        var row4 = AutoMapper.Mapper.Map<RoomAttributeViewModel>(conf);
                                        CheckinNightList.Add(row4);
                                        break;
                                    case Additional.CustomerOver:
                                        var row5 = AutoMapper.Mapper.Map<RoomAttributeViewModel>(conf);
                                        AddtionCustomerList.Add(row5);

                                        break;
                                    case Additional.PriceByHour:
                                        var row6 = AutoMapper.Mapper.Map<RoomAttributeViewModel>(conf);
                                        PriceByDayList.Add(row6);

                                        break;

                                    case Additional.PriceByDay:
                                        var row7 = AutoMapper.Mapper.Map<RoomAttributeViewModel>(conf);
                                        ConfigTime.Add(row7);

                                        break;

                                }
                            }
                            configRow.AddtionCustomerList = AddtionCustomerList;
                            configRow.CheckinDayList = CheckinDayList;
                            configRow.CheckinNightList = CheckinNightList;
                            configRow.CheckoutDayList = CheckoutDayList;
                            configRow.CheckoutNightList = CheckoutNightList;
                            configRow.PriceByDayList = PriceByDayList;
                            configRow.ConfigTime = ConfigTime;


                        }
                        var configviewModel = new ConfigPriceViewModel()
                        {
                            ConfigPriceRow = configRow,
                            ConfigType = row.ConfigType,
                            IsDefault=row.IsDefault
                        };
                        rclassModel.ConfigPrices.Add(configviewModel);
                    }



                }
                result = rclassModel;
            }
            catch (Exception ex)
            {
                this.AddError(ex.Message);
                return result;
            }

            return result;
        }
        private List<ColorStatus> ConvertToColorStatus(int hotelId)
        {
            var result = new List<ColorStatus>();
            var config = (from a in _context.HotelConfig
                          where a.HotelId == hotelId
                          select a).FirstOrDefault();

            var statusconfig = config.RoomStatusColor.Split(';');
            for (int i = 0; i < statusconfig.Length; i++)
            {
                var data = statusconfig[i].Split('_');
                var row = new ColorStatus()
                {
                    Color = data[1],
                    RoomStatus = data[0],
                    RoomStatusName = data[2]
                };
                result.Add(row);
            }
            return result;
        }

        public List<FloorRowModel> GetRoomsByFloor(int hotelId)
        {
            try
            {
                var floorRepository = unitOfWork.Repository<Floor>();
                var roomRepository = unitOfWork.Repository<Room>();
                List<FloorRowModel> result = new List<FloorRowModel>();
                var floors = floorRepository.GetMany(a => a.HotelId == hotelId && !a.IsDeleted).ToList();
                if (floors.Any())
                {

                    var config = ConvertToColorStatus(hotelId);
                    foreach (var item in floors)
                    {
                        var row = new FloorRowModel() { Name = item.Name, Id = item.Id };
                        var rooms = roomRepository.GetMany(a => a.FloorId == item.Id && !a.IsDeleted).ToList();
                        if (rooms.Any())
                        {
                            List<RoomModel> roommodel = new List<RoomModel>();
                            foreach (var room in rooms)
                            {

                                var dataRoom = AutoMapper.Mapper.Map<RoomModel>(room);
                                
                                var color = config.Where(a => a.RoomStatus == dataRoom.RoomStatus.ToString()).Select(a => a.Color).FirstOrDefault();
                                dataRoom.ColorStatus = color;
                                dataRoom.RoomClassName = unitOfWork.Repository<RoomClass>().Get(a => a.Id == room.RoomClassId).Name;
                                if (room.RoomStatus == RoomStatus.Active)
                                {
                                    var orderRoom = unitOfWork.Repository<Order>()
                                        .GetQueryable()
                                        .Where(a=>a.RoomId==room.Id && a.HotelId==room.HotelId && a.OrderStatus==(int)OrderStatus.CheckIn)
                                        .Select(a=>new OrderRoomViewModel
                                        {
                                            OrderId = a.Id,
                                            CheckInDate = a.CheckInDate.Value,
                                            CheckInTime = a.CheckInTime.Value,
                                            CustomerName = a.CustomerName,
                                            CustomerId = a.CustomerId,
                                            CompanyName = a.CompanyName,
                                            CheckOutDate = a.CheckOutDate,
                                            CheckOutTime = a.CheckOutTime.Value,
                                            CaculatorMode = a.CaculatorMode,
                                            RoomPrice = a.Price,
                                        }).FirstOrDefault();

                                   
                                    if (orderRoom != null)
                                    {
                                        var Customernum = unitOfWork.Repository<OrderCustomer>().GetQueryable().Count(a => a.OrderId == orderRoom.OrderId);
                                        orderRoom.CustomerNum = Customernum;
                                    }


                                    dataRoom.OrderRoom = orderRoom;
                                    if (orderRoom != null)
                                        dataRoom.OrderRoom.TimeSpend = CacuTimeCheckin(orderRoom.CheckInDate, orderRoom.CheckInTime);
                                }

                                roommodel.Add(dataRoom);

                            }
                            row.Rooms = roommodel;
                        }
                        result.Add(row);
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                base.AddError(ex.Message);
                return null;
            }
           
          
        }

        public List<FloorRowModel> GetRoomsByClass()
        {
            List<FloorRowModel> result = new List<FloorRowModel>();
            var roomTypes = unitOfWork.Repository<RoomClass>().GetMany(a => a.HotelId == WorkContext.BizKasaContext.HotelId && !a.IsDeleted).ToList();
            if (roomTypes.Any())
            {
                var config = ConvertToColorStatus(WorkContext.BizKasaContext.HotelId);
                foreach (var item in roomTypes)
                {

                    var rooms = unitOfWork.Repository<Room>().GetMany(c => c.RoomClassId == item.Id && !c.IsDeleted).ToList();
                    if (rooms.Any())
                    {
                        var row = new FloorRowModel() { Name = item.Name, Id = item.Id };




                        List<RoomModel> roommodel = new List<RoomModel>();
                        foreach (var room in rooms)
                        {
                            var dataRoom = AutoMapper.Mapper.Map<RoomModel>(room);

                            var color = config.Where(a => a.RoomStatus == dataRoom.RoomStatus.ToString()).Select(a => a.Color).FirstOrDefault();
                            dataRoom.ColorStatus = color;

                            if (room.RoomStatus == RoomStatus.Active)
                            {
                                var orderRoom = (from a in _context.Orders
                                                 where a.RoomId == room.Id
                                                 where a.HotelId == room.HotelId
                                                 where a.OrderStatus == (int)OrderStatus.CheckIn
                                                 select new OrderRoomViewModel
                                                 {
                                                     OrderId = a.Id,
                                                     CheckInDate = a.CheckInDate.Value,
                                                     CheckInTime = a.CheckInTime.Value,
                                                     CustomerName = a.CustomerName,
                                                     CompanyName = a.CompanyName
                                                 }).FirstOrDefault();
                                if (orderRoom != null)
                                {
                                    var Customernum = (from a in _context.OrderCustomer
                                                       where a.OrderId == orderRoom.OrderId
                                                       select a).Count();
                                    orderRoom.CustomerNum = Customernum;
                                }


                                dataRoom.OrderRoom = orderRoom;
                                if (orderRoom != null)
                                    dataRoom.OrderRoom.TimeSpend = CacuTimeCheckin(orderRoom.CheckInDate, orderRoom.CheckInTime);
                            }

                            roommodel.Add(dataRoom);

                        }
                        row.Rooms = roommodel;
                        result.Add(row);
                    }

                }

            }
            return result;
        }


        public List<RoomModel> GetRoomsByShort()
        {
            List<RoomModel> roommodel = new List<RoomModel>();
            int m_hoteId = WorkContext.BizKasaContext.HotelId;
            var rooms = unitOfWork.Repository<Room>().GetMany(c => c.HotelId == m_hoteId && !c.IsDeleted).ToList();
            if (rooms.Any())
            {
                var config = ConvertToColorStatus(m_hoteId);
                foreach (var room in rooms)
                {
                    var dataRoom = AutoMapper.Mapper.Map<RoomModel>(room);

                    var color = config.Where(a => a.RoomStatus == dataRoom.RoomStatus.ToString()).Select(a => a.Color).FirstOrDefault();
                    dataRoom.ColorStatus = color;

                    if (room.RoomStatus == RoomStatus.Active)
                    {
                        var orderRoom = unitOfWork.Repository<Order>().GetQueryable()
                            .Where(a => a.RoomId == room.Id && a.OrderStatus == (int)OrderStatus.CheckIn)
                            .Select(a => new OrderRoomViewModel
                            {
                                OrderId = a.Id,
                                CaculatorMode=a.CaculatorMode,
                                CheckInDate = a.CheckInDate.Value,
                                CheckInTime = a.CheckInTime.Value,
                                CustomerName = a.CustomerName,
                                CompanyName = a.CompanyName
                            }).FirstOrDefault();
                        
                        if (orderRoom != null)
                        {
                            var Customernum = unitOfWork.Repository<OrderCustomer>().GetQueryable().Count(a => a.OrderId == orderRoom.OrderId);
                            orderRoom.CustomerNum = Customernum;
                        }


                        dataRoom.OrderRoom = orderRoom;
                        if (orderRoom != null)
                            dataRoom.OrderRoom.TimeSpend = CacuTimeCheckin(orderRoom.CheckInDate, orderRoom.CheckInTime);
                    }

                    roommodel.Add(dataRoom);

                }
              
              
            }
            return roommodel;
        }

        public List<FloorRowModel> GetRoomsByStatus(RoomStatus status)
        {
            List<FloorRowModel> result = new List<FloorRowModel>();
            var rooms = unitOfWork.Repository<Room>().GetMany(a => a.HotelId == WorkContext.BizKasaContext.HotelId && a.RoomStatus == status).ToList();
            if (rooms.Any())
            {
                var row = new FloorRowModel() { Name = status.ToName() };
                var config = ConvertToColorStatus(WorkContext.BizKasaContext.HotelId);
                List<RoomModel> roommodel = new List<RoomModel>();
                foreach (var item in rooms)
                {
                    var dataRoom = AutoMapper.Mapper.Map<RoomModel>(item);

                    var color = config.Where(a => a.RoomStatus == dataRoom.RoomStatus.ToString()).Select(a => a.Color).FirstOrDefault();
                    dataRoom.ColorStatus = color;

                    if (item.RoomStatus == RoomStatus.Active)
                    {
                        var orderRoom = (from a in _context.Orders
                                         where a.RoomId == item.Id
                                         where a.HotelId == item.HotelId
                                         where a.OrderStatus == (int)OrderStatus.CheckIn
                                         select new OrderRoomViewModel
                                         {
                                             OrderId = a.Id,
                                             CheckInDate = a.CheckInDate.Value,
                                             CheckInTime = a.CheckInTime.Value,
                                             CustomerName = a.CustomerName,
                                             CompanyName = a.CompanyName
                                         }).FirstOrDefault();
                        if (orderRoom != null)
                        {
                            var Customernum = (from a in _context.OrderCustomer
                                               where a.OrderId == orderRoom.OrderId
                                               select a).Count();
                            orderRoom.CustomerNum = Customernum;
                        }


                        dataRoom.OrderRoom = orderRoom;
                        if (orderRoom != null)
                            dataRoom.OrderRoom.TimeSpend = CacuTimeCheckin(orderRoom.CheckInDate, orderRoom.CheckInTime);
                    }

                    roommodel.Add(dataRoom);


                }
                row.Rooms = roommodel;
                result.Add(row);
            }
            return result;
        }


        public List<RoomTypeViewModel> GetRoomUtilityBy(int roomTypeId)
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var featureMap = unitOfWork.Repository<UtilityMapping>().GetMany(a => a.HotelId == hotelId && a.RoomTypeId == roomTypeId);
            var roomtype = unitOfWork.Repository<Utility>().GetMany(a => a.UtilityType == (int)UtilitiesType.Room && !a.IsDeleted).ToList();
            var result = AutoMapper.Mapper.Map<List<RoomTypeViewModel>>(roomtype);
            foreach (var item in featureMap)
            {
                var row = result.Where(a => a.Id == item.UtilityId).FirstOrDefault();
                if(row!=null)
                    row.IsSelected = true;
            }

            return result;
        }



        public RoomStaticModel GetRoomsStaticByTime(int hotelId, RoomSearchByTimeModel model)
        {

            RoomStaticModel result = new RoomStaticModel();
            List<RoomStaticRowModel> Rooms = new List<RoomStaticRowModel>();
            List<RoomEventRowModel> Events = new List<RoomEventRowModel>();
            int count = 1;
            var floorRepository = unitOfWork.Repository<Floor>();
            var roomRepository= unitOfWork.Repository<Room>();
            var floors = floorRepository.GetMany(a => a.HotelId == hotelId && !a.IsDeleted).ToList();
            if (floors.Any())
            {
                foreach (var item in floors)
                {
                    var rooms = roomRepository.GetMany(a => a.FloorId == item.Id && !a.IsDeleted).ToList();
                    if (rooms.Any())
                    {
                        int orderDisplay = 0;
                        foreach (var room in rooms)
                        {
                            RoomStaticRowModel roomStatic = new RoomStaticRowModel();
                            roomStatic.id = room.Id.ToString();
                            roomStatic.name = "Phòng " + room.Name;
                            roomStatic.value = orderDisplay;
                            // roomStatic.eventColor = "red";
                            Rooms.Add(roomStatic);
                            result.Rooms = Rooms;
                            orderDisplay++;
                        }


                    }
                }


            }
            if (!model.FromDate.HasValue) return result;

            model.FromDate = model.FromDate.Value.ToMinDate();
            model.ToDate = model.ToDate.Value.ToMaxDate();
            var orders = (from a in _context.Orders
                          where a.HotelId == hotelId
                            && ((a.CheckInDate >= model.FromDate.Value && a.CheckInDate <= model.ToDate.Value) || (a.CheckInDate <= model.FromDate.Value && a.CheckOutDate > model.FromDate.Value))
                           && (a.OrderStatus == (int)OrderStatus.Booking || a.OrderStatus == (int)OrderStatus.CheckIn)
                          select new
                          {
                              OrderId = a.Id,
                              CheckInDate = a.CheckInDate.Value,
                              CheckOutDate = a.CheckOutDate.Value,
                              CheckInTime = a.CheckInTime.Value,
                              CustomerName = a.CustomerName,
                              CompanyName = a.CompanyName,
                              OrderStatus = a.OrderStatus,
                              RoomId = a.RoomId
                          }).ToList();


            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    RoomEventRowModel eventrom = new RoomEventRowModel();
                    eventrom.resource = order.RoomId.ToString();
                    eventrom.id = count;
                    eventrom.startDate = order.CheckInDate;
                    eventrom.endDate = order.CheckOutDate;
                    eventrom.paid = "50";
                    eventrom.text = string.IsNullOrWhiteSpace(order.CustomerName) ? order.CompanyName : order.CustomerName;
                    if (order.OrderStatus == (int)OrderStatus.Booking)
                    {
                        eventrom.status = "New";
                    }
                    else {
                        eventrom.status = "Arrived";
                        eventrom.endDate = DateTime.Now;
                    }

                       
                    Events.Add(eventrom);
                    count++;
                }

            }
            result.events = Events;
            return result;
        }




        public List<RoomsClassRowModel> GetRoomForCheckinMutil()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            List<RoomsClassRowModel> result = new List<RoomsClassRowModel>();
            var room = unitOfWork.Repository<RoomClass>().GetQueryable()
                .Where(a => a.HotelId == hotelId && !a.IsDeleted && a.Rooms.Count > 0)
                .Select(a => new RoomsClassRowModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    RoomTotal = a.Rooms.Where(c => !c.IsDeleted).Count(),
                    RoomAvailable = a.Rooms.Where(c => c.RoomStatus != RoomStatus.Active && !c.IsDeleted).Count(),
                    RoomUsed = a.Rooms.Where(c => c.RoomStatus == RoomStatus.Active && !c.IsDeleted).Count(),
                    Rooms = a.Rooms.Where(c => c.RoomStatus != RoomStatus.Active && !c.IsDeleted).Select(d => new RoomsViewAvailableModel { Id = d.Id, Name = d.Name }).ToList(),
                    listConfig = a.ConfigPrices.Where(b => b.IsActive && !b.IsDeleted).Select(e => new ConfigPriceRowModel()
                    {
                        Name = e.Name,
                        Id = e.Id,
                        PriceByDay = e.PriceByDay,
                        PriceByNight = e.PriceByNight
                    }).ToList()
                }).ToList();

            result = room;
            return result;
        }

        public void AutoGeneralFloorAndRoom(int hoteid)
        {
            var hotelRepository = unitOfWork.Repository<Hotel>();
            var floorRepository = unitOfWork.Repository<Floor>();
            var roomRepository= unitOfWork.Repository<Room>();
            var hotel = hotelRepository.GetById(hoteid);
            if (hotel != null)
            {
                if (hotel.NumFloors > 0 && hotel.NumRooms > 0)
                {


                    int roomper = hotel.NumRooms / hotel.NumFloors;
                    for (int i = 1; i <= hotel.NumFloors; i++)
                    {
                        var floor = new Floor() { HotelId = hotel.Id, Name = "Tầng " + i };
                        floorRepository.Add(floor);
                        for (int j = 1; j <= roomper; j++)
                        {
                            var room = new Room() { Floor = floor, HotelId = hotel.Id, Name = i + "0" + j, RoomClassId = 1, RoomStatus = RoomStatus.InActive };
                            roomRepository.Add(room);
                        }
                        if (i == hotel.NumFloors)
                        {
                            int num = hotel.NumRooms - (roomper * hotel.NumFloors);
                            int fl = 1;
                            while (num > 0)
                            {
                                var room = new Room() { Floor = floor, HotelId = hotel.Id, Name = i + "0" + roomper + fl, RoomStatus = RoomStatus.InActive };
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

        public List<ConfigPriceRowModel> GetConfigPriceByRoom(int roomid)
        {
            var roomClass = (from a in _context.Rooms
                             join b in _context.RoomClasses on a.RoomClassId equals b.Id
                             join c in _context.ConfigPrice on b.Id equals c.RoomClassId
                             where a.Id == roomid && !a.IsDeleted
                             select new ConfigPriceRowModel()
                             {
                                 IsActive = c.IsActive,
                                 Id = c.Id,
                                 IsDefault = c.IsDefault,
                                 Name = c.Name,
                                 PriceByDay = c.PriceByDay,
                                 PriceByNight = c.PriceByNight,
                                 RoomClassId = c.RoomClassId
                             }).ToList();
            return roomClass;
        }

     

        private string CacuTimeCheckin(DateTime checkindate, TimeSpan checkintime)
        {
            string result = string.Empty;
            if (checkindate == null)
                return result;

            var compareDate = DateTime.Now - checkindate;
            if (compareDate.Days > 0)
            {
                int days = compareDate.Days;
                if (compareDate.Hours > 0)
                {
                    days += 1;
                }
                result = string.Format("{0} ngày", days);
            }
            else
            {
                // var compareTime = DateTime.Now.Hour - checkintime.Hours;
                if (compareDate.Hours > 0)
                    result = string.Format("{0} giờ", compareDate.Hours);
                else
                {
                    //var mini = DateTime.Now.Minute - checkintime.Minutes;
                    result = string.Format("{0} phút", compareDate.Minutes);
                }
            }
            return result;
        }


        public RoomForEditModel GetRoomForEdit(int roomId)
        {
            var room = unitOfWork.Repository<Room>().GetById(roomId);
            var result = new RoomForEditModel()
            {
                FloorId = room.FloorId,
                RoomClassId = room.RoomClassId,
                RoomId = room.Id,
                RoomName = room.Name

            };
            return result;
        }





        public bool EditRoom(RoomForEditModel model)
        {
            var roomRepo = unitOfWork.Repository<Room>();
            var room = roomRepo.GetById(model.RoomId);
            room.Name = model.RoomName;
            room.FloorId = model.FloorId;
            room.RoomClassId = model.RoomClassId;
            roomRepo.Update(room);
            unitOfWork.Commit();
            return !this.HasError;
        }
    }
}
