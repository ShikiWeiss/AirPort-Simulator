using AirPort.Common.Enums;
using AirPort.Common.Models.Api;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Server.BL.Simulator.Api;
using AirPort.Services.ServerServices.Api;
using Common.Enums;
using Common.Models;
using Common.Models.Stations;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirPort.Server.BL.Simulator
{
    public class LandingSimulator : ILandingSimulator
    {
        private readonly ISimulatorService simulatorService;
        private readonly IDbSaveService dbSaveService;

        //ctor
        public LandingSimulator(ISimulatorService simulatorService, IDbSaveService dbSaveService)
        {
            this.simulatorService = simulatorService;
            this.dbSaveService = dbSaveService;
        }

        public void Land(Plane plane, LandingRunwayStation runway)
        {
            SetPlaneHistory(plane, runway);
            SetPlaneLandedProp(plane);
            simulatorService.AttachPlaneToStation(plane, runway);
            DbUpdate(plane, runway, FlightActionsEnum.Landing);
            InvokeNotifierAction(plane);
            simulatorService.TimeToSleep(1500);
        }

        private void SetPlaneLandedProp(Plane plane) => plane.IsLanded = true;

        private void SetPlaneHistory(Plane plane, LandingRunwayStation runway) => simulatorService.SetPlaneEnteredTime(plane, runway);

        private void InvokeNotifierAction(Plane plane) => Task.Run(() => simulatorService.InvokeLanding(plane));

        private void DbUpdate(Plane plane, LandingRunwayStation station, FlightActionsEnum flightAction)
        {
            ILanding flight = simulatorService.CreateFlightByAction(plane, station, flightAction) as ILanding;
            dbSaveService.AddLanding(flight);
        }
    }
}
