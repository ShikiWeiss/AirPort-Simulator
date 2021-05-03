using AirPort.Common.Models.Api.StationApi;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Services.ServerServices.Api
{
    public interface IHistoryService
    {
        PlaneToStationMovement Psm { get; }
        void SetPlaneEnteredTime(Plane plane, IStation station);
        void SetPlaneExitedTime(Plane plane);

    }
}
