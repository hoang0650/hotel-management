
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Proxy;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Bizkasa.Service.Inside
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
                result = IoC.Get<IHotelProxyService>().DisableHotel(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
