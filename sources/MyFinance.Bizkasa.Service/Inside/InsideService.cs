using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Bizkasa.Service.Inside
{
    public interface IInsideService : IInsideUserService
        , IInsideHotelService
        ,IInsideUtilityService
    {

    }
    public partial class InsideService : IInsideService
    {
    }
}
