using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Stations
{
    [Serializable]
    public class LandingRunwayStation : RunwayStation, ILandingRunway
    {

    }
}
