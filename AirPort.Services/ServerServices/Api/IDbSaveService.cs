using AirPort.Common.Models;
using AirPort.Common.Models.Api;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Services.ServerServices.Api
{
    public interface IDbSaveService
    {
         bool AddMovementHistory(PlaneToStationMovement data);

        bool AddPlane(Plane plane);

        bool AddTakeoof(ITakeoff takeoff);

        bool AddLanding(ILanding landing);
    }
}
