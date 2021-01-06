using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Proxy
{
      public interface IFloorProxyService : IBusinessBase
    {
          List<FloorModel> GetListFloor();
          bool InsertOrUpdateFloor(FloorModel model);
          FloorModel GetFloorBy(int floorId);
          bool DeleteFloor(List<int> Ids);
    }
      public class FloorProxyService :BaseProxyService, IFloorProxyService
    {
         
          public bool DeleteFloor(List<int> Ids)
          {
              string url = "api/Floor/DeleteFloor";
              return PostStructService<bool>(new { Ids = Ids }, url);
          }  
        public FloorModel GetFloorBy(int floorId)
        {
            string url = "api/Floor/GetFloorBy";
            return PostService<FloorModel>(new { Id = floorId }, url);
        }
        public bool InsertOrUpdateFloor(FloorModel model)
        {
            try
            {
                string url =  "api/Floor/InsertOrUpdateFloor";
                return PostStructService<bool>(model, url);
                
            }
            catch (Exception)
            {
                this.AddError("Lỗi lấy dữ liệu! ");
                return false;
            }
        }
        public List<FloorModel> GetListFloor()
        {
            try
            {
                string url = "api/Floor/GetListFloor";
                return GetDataService<List<FloorModel>>(url);
            }
            catch (Exception)
            {
                this.AddError("Lỗi lấy dữ liệu! ");
                return null;
            }
        }
    }
}
