using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Domain;
using MyFinance.Data.Infrastructure;
using System.Data;

namespace MyFinance.Data
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        //public void AssignRole(string userName, List<string> roleNames)
        //{
        //    var user = this.GetById(userName);
        //   // user.Roles.Clear();
        //    foreach (string roleName in roleNames)
        //    {
        //        var role = this.DataContext.Roles.Find(roleName);
        //     //   user.Roles.Add(role);
        //    }

        //    this.DataContext.Users.Attach(user);
        //    this.DataContext.Entry(user).State = EntityState.Modified;
        //}
        public bool CheckExistUserName(string email)
        {
           return  GetMany(a => a.Email == email).FirstOrDefault()!=null;
        }

        public  List<User> GetByHotelId(int hotelId)
        {
            return GetMany(a => a.HotelId == hotelId&& !a.IsDeteled).ToList();
        }


        public User Login(string email,string password)
        {
            return GetMany(a => a.Email == email && a.Password==password && a.IsApproved).FirstOrDefault();
        }
        public User GetUser(string email)
        {
            return GetMany(a => a.Email == email && a.IsApproved).FirstOrDefault();
        }
    }
       
    public interface IUserRepository : IRepository<User>
    {
        bool CheckExistUserName(string email);
        User Login(string email, string password);
        List<User> GetByHotelId(int hotelId);
        User GetUser(string email);
        //void AssignRole(string userName, List<string> roleName);        
    }
}
