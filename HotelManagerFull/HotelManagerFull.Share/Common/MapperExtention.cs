using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HotelManagerFull.Share.Common
{
    /// <summary>
    /// MapperExtention
    /// </summary>
    public static class MapperExtention
    {
        public static void InitMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}
