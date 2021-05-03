using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Common.Models.Api
{
    public interface IHistory
    {
        Plane Plane { get; set; }

        Station ToStation { get; set; }

        DateTime ExitedTime { get; set; }

        DateTime EnteredTime { get; set; }
    }
}
