using AirPort.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common
{
    public class LandingDTO
    {
        public int Id { get; set; }
        public int PlaneId { get; set; }
        public string Company { get; set; }

        public int RunwayStationId { get; set; }
        public DateTime Time { get; set; }

        public LandingDTO(Landing landing)
        {
            Company = landing.Plane.Company;
            RunwayStationId = landing.Plane.CurrentStationId;
            Time = landing.Time;
            PlaneId = landing.Plane.Id;
        }
        public LandingDTO() { }
    }
}
