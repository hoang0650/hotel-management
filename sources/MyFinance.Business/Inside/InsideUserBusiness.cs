using AutoMapper;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;
namespace MyFinance.Business.Inside
{


    public interface IInsideUserBusiness : IBusinessBase
    {
        bool AddUser(UserViewModel model);
        List<UserViewModel> GetallBy();
        bool MappingUserHotel(int hotelid, int userid);
        UserLoginViewModel Login(string username, string password);
       
        bool DeleteUser(List<int> Ids);
        UserViewModel GetUserForEdit(int userId);
        bool CheckUserExist(string username);
        List<UserViewModel> GetUsers(InvoiceFilterModel filter, out int total);
        List<UserViewModel> GetUserByHotel(int hotelId);

    }
    public class InsideUserBusiness : BusinessBase, IInsideUserBusiness
    {
        private readonly MyFinanceContext _context;
        private readonly IUnitOfWork unitOfWork;
        public InsideUserBusiness(
            MyFinanceContext context,
            IUnitOfWork _unitOfWork)
        {
          
            _context = context;
            unitOfWork = _unitOfWork;
        }
      

        public List<UserViewModel> GetUsers(InvoiceFilterModel filter, out int total)
        {
            filter.Page.currentPage--;
            List<UserViewModel> result = new List<UserViewModel>();
            filter.FromDate = filter.FromDate.HasValue ? filter.FromDate.Value.ToMinDate() : filter.FromDate;
            filter.ToDate = filter.ToDate.HasValue ? filter.ToDate.Value.ToMaxDate() : filter.ToDate;
            var m_userQuery = unitOfWork.Repository<User>().GetQueryable();
            //var invoices = from a in _context.Users
            //               select new
            //               {
            //                   CreatedDate = a.DateCreated,
            //                   Email = a.Email,
            //                   Id = a.Id,
            //                   IsActive = a.IsActive,
            //                   LastLogin = a.DateLastLogin,
            //                   HotelId = a.HotelId
                               
                             

            //               };

            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                m_userQuery = m_userQuery.Where(a => a.DateCreated >= filter.FromDate.Value && a.DateCreated <= filter.ToDate.Value);
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                m_userQuery = m_userQuery.Where(a => a.Email.Contains(filter.Keyword) );
            total = m_userQuery.Count();
         var   data = m_userQuery.OrderByDescending(a => a.DateLastLogin)
                .Skip(filter.Page.currentPage * filter.Page.pageSize)
                .Take(filter.Page.pageSize)
                .Select(a=> new UserViewModel()
                {
                    Email = a.Email
                     ,
                    IsActive = a.IsActive
                    ,
                    Id = a.Id
                    ,
                    CreatedDate = a.DateCreated
                    ,
                    LastLogin = a.DateLastLogin
                    ,
                    HotelId = a.HotelId
                })
                .ToList();

        
            return data;
        }


        public bool AddUser(UserViewModel model)
        {
            var m_userRepository = unitOfWork.Repository<User>();
            var m_insideuserRepository = unitOfWork.Repository<InsideUser>();
            if (model.Id > 0)
            {
              
                var user = m_userRepository.GetMany(a => a.Id == model.Id).FirstOrDefault();
                user.FullName = model.FullName;
                user.IsActive = model.IsActive;
                user.IsOwner = model.IsOwner;
                if (!string.IsNullOrWhiteSpace(model.Password))
                    user.Password = model.Password;
                m_userRepository.Update(user);
            }
            else
            {
                if (CheckExistUserName(model.Email))
                {
                    this.AddError("Tài khoản đã tồn tại");
                    return false;
                }
                var data = Mapper.Map<UserViewModel, InsideUser>(model);
                data.DateCreated = DateTime.Now;
                data.DateLastPasswordChange = DateTime.Now;
                data.IsApproved = true;
                m_insideuserRepository.Add(data);
            }
            IoC.Get<IUnitOfWork>().Commit();
            return true;
        }

        public bool MappingUserHotel(int hotelid,int userid)
        {
            var m_userRepository = unitOfWork.Repository<User>();
            var user = m_userRepository.GetById(userid);
            //  user.HotelId = hotelid;
            m_userRepository.Update(user);
            unitOfWork.Commit();
            return true;
        }
        public UserLoginViewModel Login(string username, string password)
        {
            var m_userRepository = unitOfWork.Repository<InsideUser>();
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
            //if (!CheckExistUserName(username))
            //{
            //    this.AddError("Tài khoản không tồn tại");
            //    return null;
            //}
         
            var data = m_userRepository.GetMany(a => a.UserName == username && a.Password == password && a.IsApproved).FirstOrDefault(); 
            if (data == null)
            {
                this.AddError("Mật khẩu không đúng !");
                return null;
            }
            var map = Mapper.Map<InsideUser, UserLoginViewModel>(data);           
            data.DateLastLogin = DateTime.Now;
            m_userRepository.Update(data);
            var token = IoC.Get<ITokenBusiness>().GenerateToken(map.Id);
            map.Token = token;
            
            unitOfWork.Commit();
           

            return map;

        }
        public bool CheckUserExist(string username)
        {
            return  CheckExistUserName(username);
               
        }

        public bool CheckExistUserName(string email)
        {
            
            return unitOfWork.Repository < User >().GetMany(a => a.Email == email).FirstOrDefault() != null;
        }

        public List<UserViewModel> GetallBy()
        {
            List<UserViewModel> data = new List<UserViewModel>();
            var result = unitOfWork.Repository<InsideUser>().GetQueryable().Select(a=> new UserViewModel()
            {
                Email = a.Email,
                CreatedDate = a.DateCreated,
                IsApproved = a.IsApproved,
                IsOwner = a.IsOwner,
                LastLogin = a.DateLastLogin
            });
           
            return data;
        }

        public List<UserViewModel> GetUserByHotel(int hotelId)
        {
           
            var result = unitOfWork.Repository<User>().GetQueryable().Where(a=>a.HotelId==hotelId && !a.IsDeteled)
                .Select(a=> new UserViewModel()
                {
                    Email = a.Email,
                    Id = a.Id,
                    CreatedDate = a.DateCreated,
                    IsApproved = a.IsApproved ,
                    IsOwner = a.IsOwner,
                    LastLogin = a.DateLastLogin
                })
                .ToList();
           
            return result;
        }


        public UserViewModel GetUserForEdit(int userId)
        {
            var result = unitOfWork.Repository<User>().GetMany(a=>a.Id==userId&& !a.IsDeteled).FirstOrDefault();
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
            var m_userRepository = unitOfWork.Repository<User>();
            var widget = m_userRepository.GetMany(a => Ids.Contains(a.Id)).ToList();
            if (widget.Any())
            {
                foreach (var item in widget)
                {
                    item.IsDeteled = true;
                    m_userRepository.Update(item);
                }
            }
            unitOfWork.Commit();
            return !this.HasError;
        }

        #region Inside
        
        #endregion

    }
}
