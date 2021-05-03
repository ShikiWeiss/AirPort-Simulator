using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using AirPort.Services.ServerServices.Api;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Services.ServerServices.Implementations
{
    public class HistoryService: IHistoryService
    {

        public PlaneToStationMovement Psm { get; private set; }

        public HistoryService() => Psm = new PlaneToStationMovement();

        public void SetPlaneEnteredTime(Plane plane, IStation station) => Psm = new PlaneToStationMovement
        {
            Plane = plane,
            EnteredTime = DateTime.Now,
            ToStation = station as Station
        };

        public void SetPlaneExitedTime(Plane plane) => Psm.ExitedTime = DateTime.Now;
    }
}
