using AirPort.Common.Enums;
using AirPort.Common.Models;
using AirPort.Common.Models.Api;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using AirPort.Server.BL.Simulator.Api;
using AirPort.Services.ServerServices.Api;
using Common.Enums;
using Common.Models;
using Common.Models.Stations;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirPort.Server.BL.Simulator
{
    public class TakeoffSimulator : ITakeoffSimulator
    {
        private readonly ISimulatorService simulatorService;
        private readonly IDbSaveService dbSaveService;

        public TakeoffSimulator(ISimulatorService simulatorService, IDbSaveService dbSaveService)
        {
            this.simulatorService = simulatorService;
            this.dbSaveService = dbSaveService;
        }

        public void Takeoff(Plane plane, TakeoffRunwayStation runway)
        {
            simulatorService.AttachPlaneToStation(plane, runway);
            AddTakeoffToDb(plane, runway);
            TakeoffTime();
            InvokeNotifierAction(plane);
            AddHistoryToDB(plane);

            Clear(plane, runway);
            RemovePlaneFromWaitersList(plane, runway);
            simulatorService.InvokeStationCleared(runway);
            simulatorService.InvokeTakeoffCompleted(runway);
        }

        private void AddHistoryToDB(Plane plane)
        {
            simulatorService.SetPlaneExitedTime(plane);
            dbSaveService.AddMovementHistory(simulatorService.GetPlaneHistory());
        }

        private void TakeoffTime() => simulatorService.TimeToSleep(3000);

        private void RemovePlaneFromWaitersList(Plane plane, TakeoffRunwayStation runway) => simulatorService.RemoveWaiterFromStation(plane, runway);

        private void Clear(Plane plane, TakeoffRunwayStation runway)
        {
            simulatorService.ClearPlaneStation(plane);
            simulatorService.ClearStationPlane(runway);
        }

        private void AddTakeoffToDb(Plane plane, TakeoffRunwayStation runway)
        {
            ITakeoff flight = simulatorService.CreateFlightByAction(plane, runway, FlightActionsEnum.Takeoff) as ITakeoff;
            dbSaveService.AddTakeoof(flight);
        }

        private void InvokeNotifierAction(Plane plane) => Task.Run(() => simulatorService.InvokeTakeoff(plane));
    }
}
