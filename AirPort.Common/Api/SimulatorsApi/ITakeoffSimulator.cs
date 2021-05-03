using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirPort.Server.BL.Simulator.Api
{
    public interface ITakeoffSimulator
    {
        void Takeoff(Plane plane, TakeoffRunwayStation runway);
    }
}
