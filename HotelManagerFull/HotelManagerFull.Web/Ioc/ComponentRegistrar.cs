using Autofac;
using HotelManagerFull.Business;
using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace HotelManagerFull.Web.Ioc
{
    /// <summary>
    /// ComponentRegistrar
    /// </summary>
    public class ComponentRegistrar : Autofac.Module
    {
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(ILoginBusiness)) }).Where(x => x.IsClass && x.Name.EndsWith("Business")).As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(IDapperRepository)) }).Where(x => x.IsClass && x.Name.EndsWith("Repository")).As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(JwtIssuerOptions)) }).Where(x => x.IsClass && x.Name.EndsWith("Options")).As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(IJwtFactory)) }).Where(x => x.IsClass && x.Name.EndsWith("Factory")).As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            //----------------------Common-----------------------------------------------
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            builder.RegisterType<DbContext>().As<DbContext>();
        }
    }
}
