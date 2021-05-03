using AirPort.Common.Enums;
using AirPort.Common.Models.Api;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Enums;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AirPort.Services.ServerServices.Api
{
    public interface ISimulatorService
    {
        /// <summary>
        /// Return next avialable station related to given station.
        /// </summary>
        /// <param name="CurrentStation"></param>
        /// <returns></returns>
        IStation GetAvailabelNextStation(IStation CurrentStation);

        /// <summary>
        /// Move the plane to given station.
        /// </summary>
        /// <param name="station"></param>
        /// <param name="plane"></param>
        void MoveToNextStation(IStation station, Plane plane);

        /// <summary>
        /// Connect between plane and station.
        /// Set plane.currentStation prop to sended station.
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="station"></param>
        void AttachPlaneToStation(Plane plane, IStation station);
        void InvokeTakeoffCompleted(IStation runway);

        /// <summary>
        /// Create Flight by given action.
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="runway"></param>
        /// <param name="flightAction"></param>
        /// <returns></returns>
        IFlight CreateFlightByAction(Plane plane, RunwayStation runway , FlightActionsEnum flightAction);
        bool CanMoveToNextStation(IStation station);
        void KeepPlaneWait(Plane plane, IEnumerable<IStation> nextStations, Action<IStation, Plane> callback);
        void RemoveWaiterFromStations(Plane plane, IEnumerable<IStation> nextStations);
        void RemoveEventListenner(IEnumerable<IStation> nextStations, Action<IStation, Plane> removeCallback);
        void InvokeStationCleared(IStation station) => station.Cleared?.Invoke(station, station.PlaneWaiters.FirstOrDefault());
        void ClearPlaneStation(Plane plane) => plane.CurrentStationId = default;
        void ClearStationPlane(IStation station) => station.CurrentPlane = null;
        void SetPlaneEnteredTime(Plane plane, IStation runway);
        void SetPlaneExitedTime(Plane plane);
        void InvokeTakeoffWaiter(Plane plane);
        void InvokeLandingWaiter(Plane plane);
        void InvokeLanding(Plane plane);
        void InvokeTakeoff(Plane plane);
        void RemoveWaiterFromStation(Plane plane, IStation station);
        PlaneToStationMovement GetPlaneHistory();
        void TimeToSleep(int ms);
    }
}
