using Airport.Utilities.Api;
using Airport.Utilities.Implementations;
using AirPort.Client.ViewModels;
using AirPort.Client.ViewModels.AirportViewModels;
using AirPort.Client.ViewModels.TablesViewModels;
using AirPort.Services.ClientServices.Api;
using AirPort.Services.ClientServices.Implementations;
using AirPort.Services.ServerServices.Api;
using AirPort.Services.ServerServices.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AirPort.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider Provider;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>();

            #region ViewModels 
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LandingWaitersViewModel>();
            services.AddSingleton<TakeoffWaitersViewModel>();
            services.AddSingleton<StationsTableViewModel>();
            services.AddSingleton<AirportViewModel>();

            services.AddTransient<StationViewModel>();
            #endregion

            #region Services
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<ISerializer, JsonSerializationService>();
            services.AddSingleton<IErrorLogger, ErrorLoger>();

            #endregion

            Provider = services.BuildServiceProvider();
            var win = Provider.GetRequiredService<MainWindow>();
            win.Show();
        }
    }
}
