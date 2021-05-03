using AirPort.Common.Models.Api.StationApi;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirPort.Services.ClientServices.Api
{
    public interface IClientService
    {

        event  Action<Plane> NewLanding;
        event  Action<Plane>  NewLandingWaiter;
        event  Action<Plane> NewTakeoff;
        event  Action<Plane>  NewTakeoffWaiter;
        event Action<Plane>  PlaneMoved;
        event Action<IStation> TakeoffCompleted;

        /// <summary>
        /// Method return all stations to client.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IStation>> GetStations();

        /// <summary>
        /// Method return all landing waiters planes.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Plane>> GetLandingWaiters();

        /// <summary>
        /// Method return all takeoffs plane.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Plane>> GetTakeoffWaiters();

        /// <summary>
        /// Method for start connection with hub.
        /// </summary>
        void Connect();
    }
}
