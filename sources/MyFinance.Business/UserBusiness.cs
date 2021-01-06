using AutoMapper;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Utils;
using MyFinance.Domain.Enum;

namespace MyFinance.Business
{


    public interface IUserBusiness : IBusinessBase
    {
        bool AddUser(UserViewModel model);
        List<UserViewModel> GetallBy();
        bool MappingUserHotel(int hotelid, int userid);
        UserLoginViewModel Login(RequestLogin model);
        List<UserViewModel> GetUserByHotel(int hotelId);
        bool DeleteUser(List<int> Ids);
        UserViewModel GetUserForEdit(int userId);
        bool CheckUserExist(string username);
        UserLoginViewModel Relogin(string email);
        int Authenticate(string userName, string password);
        UserLoginViewModel LoginAsHotel(int HotelId);
        UserLoginViewModel LoginTicket(string username, string password);
        bool Logout();
        List<UserViewModel> GetAdminByHotel(int hotelId);
    }
    public class UserBusiness : BusinessBase, IUserBusiness
    {
        private readonly IUnitOfWork unitOfWork;
      

        public UserBusiness( IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Public method to authenticate user by user name and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Authenticate(string userName, string password)
        {
            var user = unitOfWork.Repository<User>().Get(u => u.Email == userName && u.Password == password && u.IsActive && !u.IsDeteled);
            if (user != null && user.Id > 0)
            {
                return user.Id;
            }
            return 0;
        }
        public bool Logout()
        {
            var m_userReponsitory = unitOfWork.Repository<User>();
            var m_userLockeds = m_userReponsitory.GetQueryable().Where(a => a.IsLocked && a.HotelId == WorkContext.BizKasaContext.HotelId).ToList();
            // mở khóa sau khi user logout
            if (m_userLockeds.Any())
            {
                foreach (var item in m_userLockeds)
                {
                    item.IsLocked = false;
                    m_userReponsitory.Update(item);
                }
                unitOfWork.Commit();
            }
            return !this.HasError;
        }
        public List<UserViewModel> GetAdminByHotel(int hotelId)
        {
            var UserRepo = unitOfWork.Repository<User>();

            var result = UserRepo.GetQueryable().Where(a => a.HotelId == hotelId && !a.IsDeteled && a.UserType == UserType.Admin)
                .Select(a => new UserViewModel()
                {
                    Email = a.Email,
                    Id = a.Id,
                    CreatedDate = a.DateCreated,
                    IsApproved = a.IsApproved,
                    IsOwner = a.IsOwner,
                    IsInShift = a.IsInShift,
                    LastLogin = a.DateLastLogin
                })
                .ToList();

            return result;
        }
        public UserLoginViewModel Relogin(string email)
        {
            try
            {
                var UserRepo = unitOfWork.Repository<User>();
                if (string.IsNullOrWhiteSpace(email)) return null;
                var data = UserRepo.Get(a => a.Email == email && a.IsApproved && !a.IsDeteled);
                if (data == null)
                {
                    return null;
                }
                var map = Mapper.Map<User, UserLoginViewModel>(data);
                int m_hotelIsSelected = unitOfWork.Repository<OwnerHotel>().GetQueryable()
                    .Where(a => a.UserOwnerId == map.Id && a.IsSelected).Select(a => a.HotelId).FirstOrDefault();

                var hotel = unitOfWork.Repository<Hotel>().GetById(m_hotelIsSelected);
                map.HotelName = hotel.Name;
                map.HotelId = m_hotelIsSelected;
                map.Logo = hotel.Logo;
                var token = IoC.Get<ITokenBusiness>().GenerateToken(map.Id);
                map.Token = token;
                // get hotel owner
                if (data.IsOwner)
                    map.OwnerHotels = IoC.Get<IHotelBusiness>().GetHotelOwnerByUser(data.Id);
                map.ShiftId = getShift(data);
                unitOfWork.Commit();   
                return map;
            }
            catch (Exception ex)
            {
                base.AddError(ex.Message);
                return null;
                throw;
            }
           


         
        }
        public bool AddUser(UserViewModel model)
        {
            var repo = unitOfWork.Repository<User>();
            if (model.Id > 0)
            {
                var user = repo.Get(a => a.Id == model.Id && a.HotelId == model.HotelId);
                user.FullName = model.FullName;
                user.IsActive = model.IsActive;
                //user.IsOwner = model.IsOwner;
                user.UserType = model.UserType;
                if (!string.IsNullOrWhiteSpace(model.Password))
                    user.Password = model.Password;
                repo.Update(user);
            }
            else
            {
                var userExist = repo.Get(a => a.Email == model.Email);
                if (userExist!=null)
                {
                   base.AddError("Tài khoản đã tồn tại");
                    return false;
                }
                var data = Mapper.Map<UserViewModel, User>(model);
                data.DateCreated = DateTime.Now;
                data.DateLastPasswordChange = DateTime.Now;
                data.IsActive = true;
                data.IsApproved = true;
                repo.Add(data);
            }
            unitOfWork.Commit();
            return true;
        }

        public bool MappingUserHotel(int hotelid,int userid)
        {
            var UserRepo = unitOfWork.Repository<User>();
            var user = UserRepo.GetById(userid);
            user.HotelId = hotelid;
            UserRepo.Update(user);           
            unitOfWork.Commit();
            return true;
        }
        public UserLoginViewModel Login(RequestLogin model)
        {
            if(string.IsNullOrWhiteSpace(model.UserName))
            {
                this.AddError("Chưa nhập tài khoản");
                return null;
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                this.AddError("Chưa nhập mật khẩu");
                return null;
            }
            var UserRepo = unitOfWork.Repository<User>();
            
            bool isExist = UserRepo.Get(a => a.Email == model.UserName) !=null;
            if (!isExist)
            {
                this.AddError("Tài khoản không tồn tại");
                return null;
            }
          
            var data = UserRepo.Get(a => a.Email == model.UserName
                                && a.Password == model.Password
                                && a.IsApproved);

            
            if (data == null)
            {
                this.AddError("Mật khẩu không đúng !");
                return null;
            }
            if (!data.IsInShift && data.UserType == Domain.Enum.UserType.Reception)
            {
                this.AddError("Bạn không thể đăng nhập vào lúc này, Đã có tài khoản lễ tân khác đăng nhập vào hệ thống  !");
                return null;
            }

            if (data.IsLocked && data.UserType == Domain.Enum.UserType.Reception)
            {
                this.AddError("Bạn không thể đăng nhập vào lúc này, Đã có tài khoản lễ tân khác đăng nhập vào hệ thống  !");
                return null;
            }

            var map = Mapper.Map<User, UserLoginViewModel>(data);
            var hotel = unitOfWork.Repository<Hotel>().GetById(map.HotelId);
           

            map.HotelName = hotel.Name;
            map.Logo = hotel.Logo;

            // get hotel owner
            if (data.IsOwner)
            {
                map.OwnerHotels = IoC.Get<IHotelBusiness>().GetHotelOwnerByUser(data.Id);

                var m_hotelExpired = map.OwnerHotels.Where(a=>a.DateExpired.HasValue && a.DateExpired<DateTime.Now ).ToList();

                if(m_hotelExpired.Count == map.OwnerHotels.Count)
                {
                    this.AddError("Khách sạn đã hết hạn sử dụng, vui lòng lên hệ quản trị hệ thống!");
                    return null;
                }

                map.HotelId= map.OwnerHotels.Where(a => a.IsSelected).Select(a => a.HotelId).FirstOrDefault();
            }
            else
            {
                if ((hotel.DateExpired.HasValue && hotel.DateExpired < DateTime.Now)|| hotel.IsDeleted)
                {
                    this.AddError("Khách sạn đã hết hạn sử dụng hoặc không tồn tại, vui lòng lên hệ quản trị hệ thống!");
                    return null;
                }
            }

            map.ShiftId = getShift(data);

            //if (data.UserType == Domain.Enum.UserType.Reception)
            //{
            //    // lay danh sách user cung loai là Domain.Enum.UserType.Reception 
            //    var m_userReceptions = UserRepo
            //                        .GetQueryable()
            //                        .Where(a => a.UserType == Domain.Enum.UserType.Reception && a.HotelId == hotel.Id)
            //                        .ToList();

            //    if (m_userReceptions.Any())
            //    {
            //        // khóa tạm thời những user khác cho đến khi user này logout
            //        foreach (var item in m_userReceptions)
            //        {
            //            if (item.Id != data.Id)
            //            {
            //                item.IsLocked = true;
            //                UserRepo.Update(item);
            //            }

            //        }
            //    }

            //}


            data.DateLastLogin = DateTime.Now;
            UserRepo.Update(data);

            var token = IoC.Get<ITokenBusiness>().GenerateToken(map.Id);
            map.Token = token;

            unitOfWork.Commit();
            return map;

        }
        public bool CheckExistUserName(string email)
        {
            var UserRepo = unitOfWork.Repository<User>();
            return UserRepo.GetMany(a => a.Email == email).FirstOrDefault() != null;
        }
        public UserLoginViewModel LoginTicket(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                this.AddError("Chưa nhập tài khoản");
                return null;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                this.AddError("Chưa nhập mật khẩu");
                return null;
            }
            if (!CheckExistUserName(username))
            {
                this.AddError("Tài khoản không tồn tại");
                return null;
            }
            var UserRepo = unitOfWork.Repository<User>();
            var data = UserRepo.GetMany(a => a.Email == username && a.Password == password && a.IsApproved).FirstOrDefault();
            if (data == null)
            {
                this.AddError("Mật khẩu không đúng !");
                return null;
            }
            var map = Mapper.Map<User, UserLoginViewModel>(data);
            data.DateLastLogin = DateTime.Now;
            UserRepo.Update(data);
            var token = IoC.Get<ITokenBusiness>().GenerateToken(map.Id);
            map.Token = token;
            unitOfWork.Commit();
            return map;

        }

        private int getShift(User data)
        {
            var shiftRepo = unitOfWork.Repository<Shift>();
            var shiftId = shiftRepo.GetQueryable().Where(a => a.UserId == data.Id && !a.EndTime.HasValue).Select(a => a.Id).FirstOrDefault();
            if (shiftId <= 0 && data.UserType == Domain.Enum.UserType.Admin)
            {
                var shift = new Shift()
                {
                    CloseAmount = 0,
                    CreatedDate = DateTime.Now,
                    StartTime = DateTime.Now,
                    HotelId = data.HotelId,
                    UserId = data.Id,
                    OpenAmount = 0
                };
                shiftRepo.Add(shift);
                unitOfWork.Commit();
                shiftId = shift.Id;
            }
           return shiftId;
        }
        public UserLoginViewModel LoginAsHotel(int HotelId)
        {
            var UserRepo = unitOfWork.Repository<User>();
            var data = UserRepo.Get(a=>a.HotelId ==HotelId && a.IsOwner);
            var map = Mapper.Map<User, UserLoginViewModel>(data);
            var hotel = unitOfWork.Repository<Hotel>().GetById(map.HotelId);
            map.HotelName = hotel.Name;
            map.Logo = hotel.Logo;
            data.DateLastLogin = DateTime.Now;
            UserRepo.Update(data);
            var token = IoC.Get<ITokenBusiness>().GenerateToken(map.Id);
            map.Token = token;
            unitOfWork.Commit();
            return map;

        }
        public bool CheckUserExist(string username)
        {

            return CheckExistUserName(username);
               
        }

        public List<UserViewModel> GetallBy()
        {
            var UserRepo = unitOfWork.Repository<User>();
            var result = UserRepo.GetQueryable().Select(item => new UserViewModel()
            {
                Email = item.Email,
                CreatedDate = item.DateCreated,
                IsApproved = item.IsApproved,
                IsOwner = item.IsOwner,
                LastLogin = item.DateLastLogin
            }).ToList();
           
            return result;
        }

        public List<UserViewModel> GetUserByHotel(int hotelId)
        {
            var UserRepo = unitOfWork.Repository<User>();
           
            var result = UserRepo.GetQueryable().Where(a => a.HotelId == hotelId && !a.IsDeteled)
                .Select(a=> new UserViewModel()
                {
                    Email = a.Email,
                    Id = a.Id,
                    CreatedDate = a.DateCreated,
                    IsApproved = a.IsApproved,
                    IsOwner = a.IsOwner,
                    IsInShift = a.IsInShift,
                    LastLogin = a.DateLastLogin
                })
                .ToList();
           
            return result;
        }


        public UserViewModel GetUserForEdit(int userId)
        {
            var UserRepo = unitOfWork.Repository<User>();
            var result = UserRepo.GetMany(a=>a.Id==userId&& !a.IsDeteled).FirstOrDefault();
            var map = Mapper.Map<UserViewModel>(result);
            return map;
        }

        public bool DeleteUser(List<int> Ids)
        {
            if (!Ids.Any())
            {
                AddError("Bạn chưa chọn nhân viên cần xóa !");
                return false;
            }

            var w_user = unitOfWork.Repository<User>().GetMany(a => Ids.Contains(a.Id)).ToList();
            if (w_user.Any())
            {
                foreach (var item in w_user)
                {
                    if (item.IsInShift)
                    {
                        base.AddError("Cần giao ca trước khi xóa tài khoản lễ tân !");
                        return false;
                    }
                    item.IsDeteled = true;
                    unitOfWork.Repository<User>().Update(item);
                }
            }
            unitOfWork.Commit();
            return !this.HasError;
        }

        

        #region Inside

        #endregion

    }
}
