using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfCoreDemo.Part1;
using WpfCoreDemo.Part2;
using WpfCoreDemo.Part3;
using WpfCoreDemo.Part4;
using WpfCoreDemo.Part5;
using WpfCoreDemo.Utils;

namespace WpfCoreDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // Setup services
            ServiceProvider = ConfigureServices().BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = true
            });

            //Merge dictionaries
            this.AddDictionaries(ServiceProvider.GetServices<ResourceDictionary>());

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainVM>();

            mainWindow.Show();
        }

        private static IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            //Register global objects
            serviceCollection
                .AddSingleton<MainWindow>()
                .AddSingleton<MainVM>();

            serviceCollection
                .AddDemoCase<IDemoCase, Part1VM>()
                .AddDemoCase<IDemoCase, Part2VM>()
                .AddDemoCase<IDemoCase, Part3VM>()
                .AddDemoCase<IDemoCase, Part4VM>()
                .AddDemoCase<IDemoCase, Part5VM>();

            //Add more demo cases here

            return serviceCollection;
        }
    }
}