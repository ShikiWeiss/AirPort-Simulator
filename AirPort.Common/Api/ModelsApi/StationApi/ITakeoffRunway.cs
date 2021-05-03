﻿using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common.Models.Api.StationApi
{
    public interface ITakeoffRunway : IRunway
    {
        FlightActionsEnum FlightAction => FlightActionsEnum.Takeoff;

    }
}
