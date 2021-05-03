using AirPort.Common.Models.Api.StationApi;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common.Models.Api
{
    public interface ILanding : IFlight
    {
        FlightActionsEnum FlightAction => FlightActionsEnum.Landing;
    }
}
