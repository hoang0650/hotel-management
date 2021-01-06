using MyFinance.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyFinance.Utils;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Extention;
using MyFinance.Data.Infrastructure;

namespace MyFinance.Business
{
    public interface IGalleryBusiness : IBusinessBase
    {
        string UploadFile(Stream fileStream, string fileName, string code);
        bool AddImage(List<GalleryModel> model);
    }
    public class GalleryBusiness : BusinessBase, IGalleryBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        public GalleryBusiness(
            IUnitOfWork unitOfWork
            
            )
        {
            this.unitOfWork = unitOfWork;
            
        }
        public string UploadFile(Stream fileStream, string fileName, string code)
        {
            //NameValueCollection nvc = new NameValueCollection();
            //nvc.Add("code", code);

            try
            {
                fileName = fileName.Replace(" ", "_").ToAscii();
               // fileName = fileName.Replace("simg", "_");
                string resultPath =MyFinance.Utils.CommonUtil.UploadFileEx(fileStream, fileName, ConfigKey.APIUploadImage);
                return resultPath;
            }
            catch (Exception ex)
            {
                this.AddError(ex.InnerException);
                return string.Empty;
            }
        }

        public bool AddImage(List<GalleryModel> model)
        {
            if (model.Any())
            {
                foreach (var item in model)
                {
                    var row = new Gallery()
                    {
                        HotelId = WorkContext.BizKasaContext.HotelId,
                        RoomTypeId = item.RoomTypeId,
                        Url = item.Url,
                        CreatedDate = DateTime.Now,
                        UserId = WorkContext.BizKasaContext.UserId
                    };
                    unitOfWork.Repository<Gallery>().Add(row);
                  
                    
                }
                IoC.Get<IUnitOfWork>().Commit();
            }
            return !this.HasError;
        }


    }
}
