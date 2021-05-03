using AirPort.Common.Models.Api;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class PlaneToStationMovement:IHistory
    {
        public int Id { get; set; }
        public Plane Plane { get; set; }

        public Station ToStation { get; set; }

        public DateTime ExitedTime { get; set; }

        public DateTime EnteredTime { get; set; }
    }
}
