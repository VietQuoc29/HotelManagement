using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagerFull.Web.Infrastructure
{
    /// <summary>
    /// DatabaseConnection
    /// </summary>
    public static class DatabaseConnection
    {
        /// <summary>
        /// InitConnection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void InitConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(ConnectionHelper.DatabaseName.MainConnectionString.ToString()));
            });
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
        }
    }
}
