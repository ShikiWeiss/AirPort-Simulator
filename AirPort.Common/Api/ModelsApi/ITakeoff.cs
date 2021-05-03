using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common.Models.Api
{
    public interface ITakeoff:IFlight
    {
        FlightActionsEnum FlightAction => FlightActionsEnum.Takeoff;
    }
}
