using Application.Managers;
using Domain.Managers;
using log4net;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Injection;

namespace Application
{
    public class ObjectFactory : IObjectFactory
    {

        public static IUnityContainer Container { get; } = new UnityContainer();
        LotteryAppContext _context;

        public ObjectFactory(LotteryAppContext context)
        {
            _context = context;

            try
            {
                Container.AddExtension(new Diagnostic());
                Container.RegisterSingleton<IUserManager, UserManager>(new InjectionConstructor(_context));
            }
            catch(Exception e)
            {
                LogManager.Current.Log.Error("failed composing all objects in the ObjectFactory", e);
            }
        }
        public T Resolve<T>() where T : class
        {
            try
            {
                if (Container.IsRegistered<T>())
                {
                    T obj = (T)Container.Resolve(typeof(T));
                    if (obj is IConfigurable)
                    {
                        if (!((IConfigurable)obj).Configured)
                        {
                            ((IConfigurable)obj).Configure();
                        }
                    }

                    return obj;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
    }
}
