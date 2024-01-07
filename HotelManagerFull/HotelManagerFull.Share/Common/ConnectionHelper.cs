using Microsoft.Extensions.Configuration;
using System;

namespace HotelManagerFull.Share.Common
{
    /// <summary>
    /// ConnectionHelper
    /// </summary>
    public static class ConnectionHelper
    {
        public enum DatabaseName
        {
            MainConnectionString
        }

        /// <summary>
        /// GetConnectionString
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static string GetConnectionString(DatabaseName dbName, IConfiguration config)
        {
            var connectionString = config.GetConnectionString(dbName.ToString());
            if (connectionString == null)
                throw new Exception($"Connection string {dbName} not in config");

            return connectionString;
        }
    }
}
