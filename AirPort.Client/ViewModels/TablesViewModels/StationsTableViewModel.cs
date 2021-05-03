using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using AirPort.Services.ClientServices.Api;
using Common.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace AirPort.Client.ViewModels
{
    public class StationsTableViewModel : ViewModelBase
    {
        private readonly IClientService service;
        public ObservableCollection<IStation> Stations { get; set; }

        //ctor
        public StationsTableViewModel(IClientService service)
        {
            this.service = service;
            InitializeProps();
            SetEventsListeners();
        }

        private void SetEventsListeners()
        {
            service.PlaneMoved += PlaneMoved;
            service.NewTakeoff += NewTakeoff;
            service.NewLanding += NewLanding;
            service.TakeoffCompleted += TakeoffCompleted;
        }

        private void TakeoffCompleted(IStation runway)
        {
            if (runway != null)
            {
                var st = Stations.FirstOrDefault(s => s.Id == runway.Id);
                Stations.RemoveAt(st.Id - 1);
                st.CurrentPlane = null;
                Stations.Insert(st.Id - 1, st);
            }
        }

        private async void InitializeProps()
        {
            Stations = new ObservableCollection<IStation>(await service.GetStations());
        }

        private void NewLanding(Plane plane)
        {
            RemoveoldStationProp(plane);
            AddNewStationProp(plane);
            RaisePropertyChanged(() => Stations);
        }

        private void NewTakeoff(Plane plane)
        {
            RemoveoldStationProp(plane);
            AddNewStationProp(plane);
            RaisePropertyChanged(() => Stations);
        }

        private void PlaneMoved(Plane plane)
        {
            RemoveoldStationProp(plane);
            AddNewStationProp(plane);
            RaisePropertyChanged(() => Stations);
        }

        private void AddNewStationProp(Plane plane)
        {
            var newStation = Stations.FirstOrDefault(s => s.Id == plane.CurrentStationId);
            if (newStation != null)
            {
                Stations.RemoveAt(newStation.Id - 1);
                newStation.CurrentPlane = plane;
                Stations.Insert(newStation.Id - 1, newStation);
            }
        }

        private void RemoveoldStationProp(Plane plane)
        {
            var oldStation = Stations.FirstOrDefault(s => s.CurrentPlane?.Id == plane.Id);
            if (oldStation != null)
            {
                Stations.RemoveAt(oldStation.Id - 1);
                oldStation.CurrentPlane = null;
                Stations.Insert(oldStation.Id - 1, oldStation);
            }
        }
    }
}
