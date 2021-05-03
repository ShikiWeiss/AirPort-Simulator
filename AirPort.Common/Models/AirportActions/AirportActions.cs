using AirPort.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirPort.Common.Models.AirportActions
{
    public static class AirportActions
    {
        public static Action<string> LandingAction { get; set; }
        public static Action<string> LandingWaiterAction { get; set; }
        public static Action<string> TakeoffAction { get; set; }
        public static Action<string> TakeoffWaiterAction { get; set; }
        public static Action<string> PlaneMovedAction { get; set; }
        public static Action<string> TakeoffCompletedAction { get; set; }
    }
}
