using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Client.ViewModels.AirportViewModels
{
    public class StationViewModel : ViewModelBase
    {
        private static int stationCounter;
        private IStation station; public IStation Station { get => station; set { Set(ref station, value); Init(); } }

        private Plane currentPlane; public Plane CurrentPlane { get => currentPlane; set { Set(ref currentPlane, value); } }

        private string name;


        public string Name { get { return name; } set { Set(ref name, value); } }

        public StationViewModel()
        {
            MessengerInstance.Register(this, $"Station{stationCounter}", (IStation st) => Station = st);
            stationCounter++;
        }

        private void Init()
        {
            Name = $"{Station.Type} station \n id: {Station.Id}";
            SetListenners();
        }

        private void SetListenners()
        {
            MessengerInstance.Register(this, $"Leave{Station.Id}", (Plane st) => SetStationPlane(null));
            MessengerInstance.Register(this, $"Takeoff{Station.Id}", (Plane plane) => SetStationPlane(null));
            MessengerInstance.Register(this, $"Arrive{Station.Id}", (Plane plane) => SetStationPlane(plane));
            MessengerInstance.Register(this, $"Landing{Station.Id}", (Plane plane) => SetStationPlane(plane));
        }
        private void SetStationPlane(Plane plane) => Station.CurrentPlane = CurrentPlane = plane;
    }
}
