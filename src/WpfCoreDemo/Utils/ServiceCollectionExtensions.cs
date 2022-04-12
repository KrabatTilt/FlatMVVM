using Microsoft.Extensions.DependencyInjection;

namespace WpfCoreDemo.Utils
{
    internal static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Custom IServiceCollection extension that registers an IDemoCase implementation.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDemoCase<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class, IDemoCase
            where TImplementation : class, TService
        {
            //Register implementation
            serviceCollection.AddSingleton<TImplementation>(); 

            //Register implementation for interface
            serviceCollection.AddSingleton<TService, TImplementation>(sp => sp.GetRequiredService<TImplementation>());

            //Register resource dictionary from implementation
            serviceCollection.AddSingleton(sp => sp.GetRequiredService<TImplementation>().Templates);

            return serviceCollection;
        }

    }
}