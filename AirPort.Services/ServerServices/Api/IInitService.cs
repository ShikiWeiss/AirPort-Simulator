using AirPort.Common.Models.Stations;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirPort.Services.ServerServices.Api
{
    /// <summary>
    /// Service for first debuted.
    /// </summary>
    public interface IInitService
    {
        List<Plane> GetLandingWaiters();
        List<Station> GetStations();
        List<Plane> GetTakeoffWaiters();
    }
}
