using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Domain;
using MyFinance.Data.Infrastructure;
using System.Data;

namespace MyFinance.Data.Inside
{
    public class InsideUserRepository : RepositoryBase<InsideUser>, IInsideUserRepository
    {
        public InsideUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       
        public bool CheckExistUserName(string email)
        {
           return  GetMany(a => a.UserName == email).FirstOrDefault()!=null;
        }

     


        public InsideUser Login(string email, string password)
        {
            return GetMany(a => a.UserName == email && a.Password==password && a.IsApproved).FirstOrDefault();
        }
    }

    public interface IInsideUserRepository : IRepository<InsideUser>
    {
        bool CheckExistUserName(string email);
        InsideUser Login(string email, string password);
       
        //void AssignRole(string userName, List<string> roleName);        
    }
}
