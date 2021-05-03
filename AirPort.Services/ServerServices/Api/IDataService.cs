using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirPort.Services.ServerServices.Api
{
    /// <summary>
    /// Service should **suplly** data to Hub.
    /// </summary>
    public interface IDataService
    {
        Task<string> GetLandingWaitersJson();

        Task<List<Plane>> GetTakeoffWaiters();

        Task<List<Plane>> GetLandingWaiters();

        Task<string> GetStationsJson();

        Task<string> GetTakeoffWaitersJson();

        Task<List<Station>> GetStations();

        List<LandingRunwayStation> GetLandingRunways();
        List<TakeoffRunwayStation> GetTakeoffRunways();

        Station GetStationById(int id);
    }
}
