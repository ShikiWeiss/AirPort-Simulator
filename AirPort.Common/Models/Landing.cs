using AirPort.Common.Models.Api;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common.Models
{
    public class Landing : ILanding 
    {
        public int Id { get; set; }

        public Plane Plane { get; set; }

        public DateTime Time { get; set; }

        public RunwayStation RunwayStation { get; set; }
    }
}
