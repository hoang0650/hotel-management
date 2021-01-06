using AutoMapper;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFinance.Bizkasa.Inside.Maps
{
    public class Maps
    {
        public static void Boot()
        {
            Mapper.CreateMap<UserViewModel, InsideUser>();

            Mapper.CreateMap<InsideUser, UserLoginViewModel>();
            Mapper.CreateMap<User, UserViewModel>()
                 .ForMember(x => x.Password, y => y.Ignore());

            Mapper.CreateMap<Utility,UtilityModel>();
           
            
        }
    }
}