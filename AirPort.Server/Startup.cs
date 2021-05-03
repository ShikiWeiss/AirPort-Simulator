using Airport.Dal;
using Airport.Dal.Api;
using Airport.Utilities.Api;
using Airport.Utilities.Implementations;
using AirPort.Common;
using AirPort.Common.Models.Stations;
using AirPort.Server.BL;
using AirPort.Server.BL.Api;
using AirPort.Server.BL.Simulator;
using AirPort.Server.BL.Simulator.Api;
using AirPort.Server.Hubs;
using AirPort.Services.ServerServices.Api;
using AirPort.Services.ServerServices.Implementations;
using Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Implementations;

namespace AirPort.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AirportContext>(opt => opt.UseSqlite(@"Data Source=AirportDB.db"), ServiceLifetime.Singleton);
            services.AddControllers();
            
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = null;
            });

            #region Simulators

            services.AddSingleton<IMainSimulator, MainSimulator>();
            services.AddSingleton<ILandingSimulator, LandingSimulator>();
            services.AddSingleton<ITakeoffSimulator, TakeoffSimulator>();

            #endregion

            #region Services

            services.AddSingleton<INotifyService, NotifyService>();
            services.AddSingleton<ISerializer, JsonSerializationService>();
            services.AddSingleton<ISimulatorService, SimulatorService>();

            services.AddSingleton<IErrorLogger, ErrorLoger>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IInitService, InitService>();
            services.AddSingleton<IDbSaveService, DbSaveService>();

            services.AddTransient<IHistoryService, HistoryService>();

            #endregion

            #region Repositories

            services.AddSingleton<IUnitRepositories, AirportRepository>();
            services.AddSingleton<IRepository<LandingDTO>, BaseRepository<LandingDTO>>();
            services.AddSingleton<IRepository<TakeoffDTO>, BaseRepository<TakeoffDTO>>();
            services.AddSingleton<IRepository<PlaneToStationMovementDTO>, BaseRepository<PlaneToStationMovementDTO>>();
            services.AddSingleton<IRepository<Station>, BaseRepository<Station>>();
            services.AddSingleton<IRepository<Plane>, BaseRepository<Plane>>();
            services.AddSingleton<IRepository<Station>, BaseRepository<Station>>();
            #endregion

            services.AddSingleton<IGenerator, Generator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AirportContext airportContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AirPortHub>("/AirPortHub");
            });
        }
    }
}
