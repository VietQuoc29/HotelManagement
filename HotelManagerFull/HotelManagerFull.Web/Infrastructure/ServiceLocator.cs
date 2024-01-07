using Microsoft.Extensions.DependencyInjection;
using System;

namespace HotelManagerFull.Web.Infrastructure
{
    /// <summary>
    /// ServiceLocator
    /// </summary>
    public class ServiceLocator
    {
        private readonly ServiceProvider _currentServiceProvider;
        private static ServiceProvider _serviceProvider;

        /// <summary>
        /// ServiceLocator
        /// </summary>
        /// <param name="currentServiceProvider"></param>
        public ServiceLocator(ServiceProvider currentServiceProvider)
        {
            _currentServiceProvider = currentServiceProvider;
        }

        /// <summary>
        /// ServiceLocator
        /// </summary>
        public static ServiceLocator Current
        {
            get
            {
                return new ServiceLocator(_serviceProvider);
            }
        }

        /// <summary>
        /// SetLocatorProvider
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void SetLocatorProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// GetInstance
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetInstance(Type serviceType)
        {
            return _currentServiceProvider.GetService(serviceType);
        }

        /// <summary>
        /// GetInstance
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService GetInstance<TService>()
        {
            return _currentServiceProvider.GetService<TService>();

        }
    }
}
