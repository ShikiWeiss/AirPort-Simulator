using Airport.Dal.Api;
using AirPort.Common.Models;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using AirPort.Services.ServerServices.Api;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPort.Services.ServerServices.Implementations
{
    public class DataService : IDataService
    {
        #region Services 

        private readonly IUnitRepositories airportRepository;
        private readonly IInitService initService;
        private readonly ISerializer jsonSerializerService;

        #endregion

        private List<Station> stations;

        //ctor
        public DataService(IUnitRepositories airportRepository, IInitService initService, ISerializer jsonSerializerService)
        {
            this.airportRepository = airportRepository;
            this.initService = initService;
            this.jsonSerializerService = jsonSerializerService;
            stations = new List<Station>();
        }


        public Task<List<Plane>> GetLandingWaiters()
        {
            //check in airportRepo if null/Empty => initService
            //var res = airportRepository.PlanesRepo?.GetAll()?.Where(p => p.IsLanded == false).ToList();
            //if (res != null) return Task.Run(() => res);
            return Task.Run(() => initService.GetLandingWaiters());
        }

        public async Task<string> GetLandingWaitersJson()
        {
            var res = await GetLandingWaiters();
            return await Task.Run(() => jsonSerializerService.Serialize(res));
        }

        public async Task<string> GetStationsJson()
        {
            var res = await GetStations();
            return await Task.Run(() => jsonSerializerService.Serialize(res));
        }

        public async Task<List<Station>> GetStations() => await Task.Run(() => initService.GetStations());

        public Task<List<Plane>> GetTakeoffWaiters() => Task.Run(() => initService.GetTakeoffWaiters());

        public async Task<string> GetTakeoffWaitersJson()
        {
            var res = await GetTakeoffWaiters();
            return await Task.Run(() => jsonSerializerService.Serialize(res));
        }

        public List<LandingRunwayStation> GetLandingRunways() => GetStations().Result.Where(s => s is LandingRunwayStation).Cast<LandingRunwayStation>().ToList();

        public List<TakeoffRunwayStation> GetTakeoffRunways() => GetStations().Result.Where(s => s is TakeoffRunwayStation).Cast<TakeoffRunwayStation>().ToList();

        public Station GetStationById(int id) => GetStations().Result.FirstOrDefault(s => s.Id == id);

    }
}
