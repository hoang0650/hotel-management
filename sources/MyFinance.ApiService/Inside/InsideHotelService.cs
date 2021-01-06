using MyFinance.Business;
using MyFinance.Business.Bizkasa.Inside;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ApiService.Inside
{
    public interface IInsideHotelService
    {
        Response<bool> DisableHotel(int hotelId);
    }
    public partial class InsideService 
    {
        public Response<bool> DisableHotel(int hotelId)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInsideHotelBusiness>().DisableHotel(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
