using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common
{
    public class PlaneToStationMovementDTO
    {
        public int Id { get; set; }
        public int PlaneId { get; set; }

        public int ToStationId { get; set; }

        public DateTime ExitedTime { get; set; }

        public DateTime EnteredTime { get; set; }
        public PlaneToStationMovementDTO(PlaneToStationMovement planeToStation)
        {
            PlaneId = planeToStation.Plane.Id;
            ToStationId = planeToStation.ToStation.Id;
            ExitedTime = planeToStation.ExitedTime;
            EnteredTime = planeToStation.EnteredTime;
        }
        public PlaneToStationMovementDTO() { }
    }
}
