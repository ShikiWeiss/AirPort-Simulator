using AirPort.Common.Enums;
using AirPort.Common.Models;
using AirPort.Common.Models.Api;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using AirPort.Services.ServerServices.Api;
using Common.Enums;
using Common.Models;
using Common.Models.Stations;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirPort.Services.ServerServices.Implementations
{
    public class SimulatorService : ISimulatorService
    {
        private readonly INotifyService notifyService;
        private readonly IDataService dataService;
        private readonly IHistoryService historyService;

        //ctor
        public SimulatorService(INotifyService notifyService, IDataService dataService, IHistoryService historyService)
        {
            this.notifyService = notifyService;
            this.dataService = dataService;
            this.historyService = historyService;
        }

        public void TimeToSleep(int ms) => Thread.Sleep(ms);

        public void AttachPlaneToStation(Plane plane, IStation station)
        {
            plane.CurrentStationId = station.Id;
            station.CurrentPlane = plane;
        }

        public IFlight CreateFlightByAction(Plane plane, RunwayStation runway, FlightActionsEnum flightAction)
        {
            return flightAction switch
            {
                FlightActionsEnum.Landing => new Landing { Plane = plane, RunwayStation = runway, Time = DateTime.Now },
                FlightActionsEnum.Takeoff => new Takeoff { Plane = plane, RunwayStation = runway, Time = DateTime.Now },
                _ => null
            };
        }

        public IStation GetAvailabelNextStation(IStation currentStation) => currentStation.NextStations?.FirstOrDefault(s => s.CurrentPlane == null);

        public void MoveToNextStation(IStation station, Plane plane)
        {
            AttachPlaneToStation(plane, station);
            TimeToSleep(2000);
            notifyService.InvokePlaneMoved(plane);
        }

        public bool CanMoveToNextStation(IStation station) => GetAvailabelNextStation(station) != null;

        public void KeepPlaneWait(Plane plane, IEnumerable<IStation> nextStations, Action<IStation, Plane> callback)
        {
            foreach (IStation st in nextStations)
            {
                st.PlaneWaiters.Add(plane);
                st.Cleared ??= callback;
            }
        }

        public void RemoveWaiterFromStations(Plane plane, IEnumerable<IStation> nextStations)
        {
            foreach (IStation st in nextStations)
                st.PlaneWaiters.Remove(plane);
        }
        public void RemoveWaiterFromStation(Plane plane, IStation station) => station.PlaneWaiters.Remove(plane);
        public void RemoveEventListenner(IEnumerable<IStation> nextStations, Action<IStation, Plane> removeCallback)
        {
            foreach (IStation st in nextStations)
                st.Cleared -= removeCallback;
        }

        public void InvokeTakeoffWaiter(Plane plane) => notifyService.InvokeTakeoffWaiter(plane);

        public void InvokeLandingWaiter(Plane plane) => notifyService.InvokeLandingWaiter(plane);

        public void InvokeLanding(Plane plane) => notifyService.InvokeLanding(plane);

        public void InvokeTakeoff(Plane plane) => notifyService.InvokeTakeoff(plane);

        public void SetPlaneEnteredTime(Plane plane, IStation station) => historyService.SetPlaneEnteredTime(plane, station);

        public void SetPlaneExitedTime(Plane plane) => historyService.SetPlaneExitedTime(plane);

        public PlaneToStationMovement GetPlaneHistory() => historyService.Psm;

        public void InvokeTakeoffCompleted(IStation runway) => notifyService.InvokeTakeoffCompleted(runway);
    }
}
