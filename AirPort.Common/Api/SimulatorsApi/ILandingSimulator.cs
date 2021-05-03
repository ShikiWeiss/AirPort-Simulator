using AirPort.Common.Models.Api.StationApi;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirPort.Server.BL.Simulator.Api
{
    public interface ILandingSimulator
    {
        void Land(Plane plane, LandingRunwayStation runway);
    }
}
