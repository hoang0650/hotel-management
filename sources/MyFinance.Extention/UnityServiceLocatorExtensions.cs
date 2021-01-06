using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyFinance.Extention
{
  public static  class UnityServiceLocatorExtensions
    {
        public static T GetInstance<T>(this IServiceLocator locator, ParameterOverride para)
        {
            IUnityContainer container = locator.GetInstance<IUnityContainer>();
            return container.Resolve<T>(para);
        }
        public static T GetInstance<T>(this IServiceLocator locator, string name, ParameterOverride para)
        {
            IUnityContainer container = locator.GetInstance<IUnityContainer>();
            return container.Resolve<T>(name, para);
        }
    }
  public class HttpContextLifetimeManager<T> : LifetimeManager, IDisposable
  {
      public override object GetValue()
      {
          return HttpContext.Current.Items[typeof(T).AssemblyQualifiedName];
      }
      public override void RemoveValue()
      {
          HttpContext.Current.Items.Remove(typeof(T).AssemblyQualifiedName);
      }
      public override void SetValue(object newValue)
      {
          HttpContext.Current.Items[typeof(T).AssemblyQualifiedName] = newValue;
      }
      public void Dispose()
      {
          RemoveValue();
      }
  }
}
