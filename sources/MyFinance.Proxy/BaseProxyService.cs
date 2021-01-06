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
    public class BaseProxyService : BusinessBase
    {
       public  T PostService<T>(object model,string url) where T : class
       {
           try
           {
               var baseAddress = Utils.ConfigKey.URLAPI + url;
               var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
               http.Accept = "application/json";
               http.ContentType = "application/json";
               http.Headers.Add("TokenId:" + WorkContext.BizKasaContext.TokenId);
               http.Method = "POST";
               var jsonstr = JsonConvert.SerializeObject(model);

               string parsedContent = jsonstr;

               UTF8Encoding encoding = new UTF8Encoding();
               Byte[] bytes = encoding.GetBytes(parsedContent);

               Stream newStream = http.GetRequestStream();
               newStream.Write(bytes, 0, bytes.Length);
               newStream.Close();

               var response = http.GetResponse();

               var stream = response.GetResponseStream();
               var sr = new StreamReader(stream);
               var data = sr.ReadToEnd();

               var result = (MyFinance.Utils.Response<T>)JsonConvert.DeserializeObject<MyFinance.Utils.Response<T>>(data);
               if (result.HasError)
               {
                   this.AddError(result.ToErrorMsg());
                   return null;
               }
               return result.Data;
           }
           catch (Exception ex)
           {

               this.AddError(ex.Message);
               return null;
           }
       }

       public StoreTokenModel PostExternalService(string code, string url)
       {
           try
           {
               var baseAddress =  url;
               var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
               http.ContentType = "application/x-www-form-urlencoded";
               //http.Headers.Add("TokenId:" + WorkContext.BizKasaContext.TokenId);
               http.Method = "POST";
              // var jsonstr = JsonConvert.SerializeObject(model);

               string parsedContent =@"client_id=99365690f16b5afbe46edad6d04eae6f&client_secret=a467e222ff8cfa7402d9ecd4978bed58&code=" + code;

               UTF8Encoding encoding = new UTF8Encoding();
               Byte[] bytes = encoding.GetBytes(parsedContent);

               Stream newStream = http.GetRequestStream();
               newStream.Write(bytes, 0, bytes.Length);
               newStream.Close();

               var response = http.GetResponse();

               var stream = response.GetResponseStream();
               var sr = new StreamReader(stream);
               var data = sr.ReadToEnd();

               var result = (StoreTokenModel)JsonConvert.DeserializeObject<StoreTokenModel>(data);
              
               return result;
           }
           catch (Exception ex)
           {

               this.AddError(ex.Message);
               return null;
           }
       }


       public bool CheckValidTokenService(string token, string url)
       {
           try
           {
               var baseAddress = url;
               var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
               http.ContentType = "application/x-www-form-urlencoded";
               http.Headers.Add("X-Bizweb-Access-Token:" + token);
               http.Method = "GET";
              WebResponse response = http.GetResponse();
               
               var stream = response.GetResponseStream();
               
               var sr = new StreamReader(stream);
               var data = sr.ReadToEnd();             
               return true;

              
           }
           catch (Exception ex)
           {
               return false;
           }
       }
       public T PostNonTokenService<T>(object model, string url) where T : class
       {
           try
           {
               var baseAddress = Utils.ConfigKey.URLAPI + url;
               var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
               http.Accept = "application/json";
               http.ContentType = "application/json";
               http.Method = "POST";
               var jsonstr = JsonConvert.SerializeObject(model);

               string parsedContent = jsonstr;

               UTF8Encoding encoding = new UTF8Encoding();
               Byte[] bytes = encoding.GetBytes(parsedContent);

               Stream newStream = http.GetRequestStream();
               newStream.Write(bytes, 0, bytes.Length);
               newStream.Close();

               var response = http.GetResponse();

               var stream = response.GetResponseStream();
               var sr = new StreamReader(stream);
               var data = sr.ReadToEnd();

               var result = (MyFinance.Utils.Response<T>)JsonConvert.DeserializeObject<MyFinance.Utils.Response<T>>(data);
               if (result.HasError)
               {
                   this.AddError(result.ToErrorMsg());
                   return null;
               }
               return result.Data;
           }
           catch (Exception ex)
           {

               this.AddError(ex.Message);
               return null;
           }
       }

       public T PostNonTokenStructService<T>(object model, string url) where T : struct
       {
           try
           {
               var baseAddress = Utils.ConfigKey.URLAPI + url;
               var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
               http.Accept = "application/json";
               http.ContentType = "application/json";
               http.Method = "POST";
               var jsonstr = JsonConvert.SerializeObject(model);

               string parsedContent = jsonstr;

               UTF8Encoding encoding = new UTF8Encoding();
               Byte[] bytes = encoding.GetBytes(parsedContent);

               Stream newStream = http.GetRequestStream();
               newStream.Write(bytes, 0, bytes.Length);
               newStream.Close();

               var response = http.GetResponse();

               var stream = response.GetResponseStream();
               var sr = new StreamReader(stream);
               var data = sr.ReadToEnd();

               var result = (MyFinance.Utils.Response<T>)JsonConvert.DeserializeObject<MyFinance.Utils.Response<T>>(data);
               if (result.HasError)
               {
                   this.AddError(result.ToErrorMsg());
                   return default(T);
               }
               return result.Data;
           }
           catch (Exception ex)
           {

               this.AddError(ex.Message);
               return default(T);
           }
       }


       public T PostOutTotalService<T>(object model, string url, out int Total ) where T : class
       {
           try
           {
               var baseAddress = Utils.ConfigKey.URLAPI + url;
               var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
               http.Accept = "application/json";
               http.ContentType = "application/json";
               http.Headers.Add("TokenId:" + WorkContext.BizKasaContext.TokenId);
               http.Method = "POST";
               var jsonstr = JsonConvert.SerializeObject(model);

               string parsedContent = jsonstr;

               UTF8Encoding encoding = new UTF8Encoding();
               Byte[] bytes = encoding.GetBytes(parsedContent);

               Stream newStream = http.GetRequestStream();
               newStream.Write(bytes, 0, bytes.Length);
               newStream.Close();

               var response = http.GetResponse();

               var stream = response.GetResponseStream();
               var sr = new StreamReader(stream);
               var data = sr.ReadToEnd();

               var result = (MyFinance.Utils.Response<DataPaging<T>>)JsonConvert.DeserializeObject<MyFinance.Utils.Response<DataPaging<T>>>(data);
               if (result.HasError)
               {
                   this.AddError(result.ToErrorMsg());
                   Total = 0;
                   return null;

               }
               Total = result.Data.TotalRecord;
               return result.Data.Data;
           }
           catch (Exception ex)
           {
               Total = 0;
               this.AddError(ex.Message);
               return null;
           }
       }
       public T PostStructService<T>(object model, string url) where T : struct
       {
           try
           {
               var baseAddress = Utils.ConfigKey.URLAPI + url;
               var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
               http.Accept = "application/json";
               http.ContentType = "application/json";
               http.Headers.Add("TokenId:" + WorkContext.BizKasaContext.TokenId);
               http.Method = "POST";
               var jsonstr = JsonConvert.SerializeObject(model);

               string parsedContent = jsonstr;

               UTF8Encoding encoding = new UTF8Encoding();
               Byte[] bytes = encoding.GetBytes(parsedContent);

               Stream newStream = http.GetRequestStream();
               newStream.Write(bytes, 0, bytes.Length);
               newStream.Close();

               var response = http.GetResponse();

               var stream = response.GetResponseStream();
               var sr = new StreamReader(stream);
               var data = sr.ReadToEnd();

               var result = (MyFinance.Utils.Response<T>)JsonConvert.DeserializeObject<MyFinance.Utils.Response<T>>(data);
               if (result.HasError)
               {
                   this.AddError(result.ToErrorMsg());
                   return default(T) ;
               }
               return result.Data;
           }
           catch (Exception ex)
           {

               this.AddError(ex.Message);
               return default(T);
           }
       }
       public  T GetDataService<T>(string url) where T : class
       {
           try
           {
               var baseAddress = Utils.ConfigKey.URLAPI + url;

               var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
               http.Accept = "application/json";
               http.Headers.Add("TokenId:" + WorkContext.BizKasaContext.TokenId);
               http.ContentType = "application/json";
               http.Method = "get";

               var response = http.GetResponse();

               var stream = response.GetResponseStream();
               var sr = new StreamReader(stream);
               var data = sr.ReadToEnd();

               var result = (MyFinance.Utils.Response<T>)JsonConvert.DeserializeObject<MyFinance.Utils.Response<T>>(data);
               if (result.HasError)
               {
                   this.AddError(result.ToErrorMsg());
                   return null;
               }
               return result.Data;
           }
           catch (Exception ex)
           {

               this.AddError(ex.Message);
               return null;
           }
       }
    }
}
