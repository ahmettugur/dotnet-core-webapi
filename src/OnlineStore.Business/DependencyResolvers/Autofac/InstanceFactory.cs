using System;
using Autofac;

namespace OnlineStore.Business.DependencyResolvers.Autofac
{
    public class InstanceFactory
    {
        protected InstanceFactory()
        {
            
        }
        public static T GetInstance<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacBusinessModule());
            var container = builder.Build();

            return container.Resolve<T>();
        }
    }
}
