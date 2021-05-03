using AirPort.Common.Enums;
using AirPort.Common.Models.Stations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AirPort.Common.Models.Api.StationApi
{
    public interface IStation : INotifyPropertyChanged
    {
        int Id { get; set; }
        object Locker { get; }

        Action<IStation, Plane> Cleared { get; set; }

        Plane CurrentPlane { get; set; }

        List<Plane> PlaneWaiters { get; set; }

        List<Station> NextStations { get; set; }
        StationType Type { get; set; }
    }
}
