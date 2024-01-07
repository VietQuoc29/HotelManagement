using Microsoft.Extensions.Configuration;
using System;

namespace HotelManagerFull.Share.Common
{
    /// <summary>
    /// Configuration
    /// </summary>
    public class Configuration
    {
        private static Configuration _instance;
        private static IConfiguration _config;

        private Configuration()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false, true)
                .Build();
        }

        public static Configuration Instance => _instance ?? (_instance = new Configuration());

        /// <summary>
        /// GetConfig
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfig(string key)
        {
            return _config[key];
        }

        /// <summary>
        /// GetConfig
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfig(string section, string key)
        {
            return _config[$"{section}:{key}"];
        }

        /// <summary>
        /// GetConfig
        /// </summary>
        /// <param name="section"></param>
        /// <param name="subSection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfig(string section, string subSection, string key)
        {
            return _config[$"{section}:{subSection}:{key}"];
        }

        /// <summary>
        /// GetConfig
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetConfig<T>(string key)
        {
            return (T)Convert.ChangeType(_config[key], typeof(T));
        }

        /// <summary>
        /// GetConfig
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetConfig<T>(string section, string key)
        {
            return (T)Convert.ChangeType(_config[$"{section}:{key}"], typeof(T));
        }

        /// <summary>
        /// GetConfig
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="subSection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetConfig<T>(string section, string subSection, string key)
        {
            return (T)Convert.ChangeType(_config[$"{section}:{subSection}:{key}"], typeof(T));
        }

        /// <summary>
        /// GetSection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <returns></returns>
        public T GetSection<T>(string section)
        {
            return _config.GetSection(section).Get<T>();
        }

        /// <summary>
        /// GetSection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="subSection"></param>
        /// <returns></returns>
        public T GetSection<T>(string section, string subSection)
        {
            return _config.GetSection(section).GetSection(subSection).Get<T>();
        }
    }
}
