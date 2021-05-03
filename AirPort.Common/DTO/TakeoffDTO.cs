using AirPort.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common
{
    public class TakeoffDTO
    {
        public int Id { get; set; }

        public int PlaneId { get; set; }

        public DateTime Time { get; set; }

        public int RunwayStationId { get; set; }

        public TakeoffDTO(Takeoff takeoff)
        {
            PlaneId = takeoff.Plane.Id;
            Time = takeoff.Time;
            RunwayStationId = takeoff.RunwayStation.Id;
        }

        public TakeoffDTO() { }
    }
}
