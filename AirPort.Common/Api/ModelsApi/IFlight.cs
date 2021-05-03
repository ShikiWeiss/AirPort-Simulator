using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common.Models.Api
{
    public interface IFlight
    {
        int Id { get; set; }

        Plane Plane { get; set; }

        DateTime Time { get; set; }

        RunwayStation RunwayStation { get; set; }
    }
}
