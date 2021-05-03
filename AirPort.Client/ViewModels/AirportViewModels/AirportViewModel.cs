using AirPort.Client.Views.AirportViews;
using AirPort.Common.Enums;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Services.ClientServices.Api;
using Common.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AirPort.Client.ViewModels.AirportViewModels
{
    public class AirportViewModel : ViewModelBase
    {
        private readonly IClientService clientService;

        private Grid mainGrid; public Grid MainGrid { get => mainGrid; set => Set(ref mainGrid, value); }

        private List<IStation> stations; public List<IStation> Stations { get => stations; set { stations = value; BuildAirport(); } }

        public AirportViewModel(IClientService clientService)
        {
            this.clientService = clientService;
            InitializeProps();
            SetEventsListeners();
        }

        private async void InitializeProps()
        {
            MainGrid = new Grid();
            Stations = new List<IStation>(await clientService.GetStations());
        }

        private void SetEventsListeners()
        {
            clientService.NewLanding += OnNewLanding;
            clientService.NewTakeoff += OnNewTakeoff;
            clientService.PlaneMoved += OnPlaneMove;
        }

        private void OnPlaneMove(Plane plane)
        {
            IStation leftStation = GetPreStation(plane);
            IStation arriveStation = GetCurrentStation(plane);

            InvokeLeaveStation(plane, leftStation);
            InvokeArriveStation(plane, arriveStation);
        }

        private void InvokeArriveStation(Plane plane, IStation arriveStation)
        {
            if (arriveStation != null) InvokePlaneMessage(plane, $"Arrive{arriveStation.Id}");
        }

        private void InvokeLeaveStation(Plane plane, IStation leftStation)
        {
            if (leftStation != null) InvokePlaneMessage(plane, $"Leave{leftStation.Id}");
        }

        private IStation GetCurrentStation(Plane plane) => Stations.FirstOrDefault(s => s.Id == plane.CurrentStationId);

        private IStation GetPreStation(Plane plane) => Stations.Where(s => s.CurrentPlane != null).FirstOrDefault(s => s.CurrentPlane.Id == plane.Id);

        private void OnNewTakeoff(Plane plane) => InvokePlaneMessage(plane, $"Takeoff{plane.CurrentStationId}");
        private void OnNewLanding(Plane plane) => InvokePlaneMessage(plane, $"Landing{plane.CurrentStationId}");

        private void InvokePlaneMessage(Plane plane, string token) => MessengerInstance.Send(plane, token);

        private void BuildAirport()
        {

            Grid runwaysGrid = SetGridColumns(2);

            Grid middlesGrid = SetGridColumns(2);

            SetRowsMainGrid();

            StackPanel landingsRunwaysSP, takeoffsRunwaysSP, landingsMiddlesSP, takeoffsMiddlesSP, hangarsSP;

            CreateLayersStackPanels(out landingsRunwaysSP, out takeoffsRunwaysSP, out landingsMiddlesSP, out takeoffsMiddlesSP, out hangarsSP);

            SetStationsLayers(landingsRunwaysSP, takeoffsRunwaysSP, landingsMiddlesSP, takeoffsMiddlesSP, hangarsSP);

            AddRunwayLayerElements(runwaysGrid, landingsRunwaysSP, takeoffsRunwaysSP);

            AddMiddleLayerElements(middlesGrid, landingsMiddlesSP, takeoffsMiddlesSP);

            AddMainGridElements(runwaysGrid, middlesGrid, hangarsSP);
        }

        private void AddMainGridElements(Grid runwaysGrid, Grid middlesGrid, StackPanel hangarsSP)
        {
            AddUIElementToGrid(runwaysGrid, MainGrid, 0, 0);
            AddUIElementToGrid(middlesGrid, MainGrid, 0, 1);
            AddUIElementToGrid(hangarsSP, MainGrid, 0, 2);
        }

        private void AddMiddleLayerElements(Grid middlesGrid, StackPanel landingsMiddlesSP, StackPanel takeoffsMiddlesSP)
        {
            AddUIElementToGrid(takeoffsMiddlesSP, middlesGrid, 0, 0);
            AddUIElementToGrid(landingsMiddlesSP, middlesGrid, 1, 0);
        }

        private void AddRunwayLayerElements(Grid runwaysGrid, StackPanel landingsRunwaysSP, StackPanel takeoffsRunwaysSP)
        {
            AddUIElementToGrid(takeoffsRunwaysSP, runwaysGrid, 0, 0);
            AddUIElementToGrid(landingsRunwaysSP, runwaysGrid, 1, 0);
        }

        private void SetStationsLayers(StackPanel landingsRunwaysSP, StackPanel takeoffsRunwaysSP, StackPanel landingsMiddlesSP, StackPanel takeoffsMiddlesSP, StackPanel hangarsSP)
        {
            for (int i = 0; i < Stations.Count; i++)
            {
                AddStationToLayerByType(landingsRunwaysSP, takeoffsRunwaysSP, landingsMiddlesSP, takeoffsMiddlesSP, hangarsSP, i);
                SendStation(i, Stations[i]);
            }
        }

        private void AddStationToLayerByType(StackPanel landingsRunwaysSP, StackPanel takeoffsRunwaysSP, StackPanel landingsMiddlesSP, StackPanel takeoffsMiddlesSP, StackPanel hangarsSP, int i)
        {
            switch (Stations[i].Type)
            {
                case StationType.Hangar:
                    hangarsSP.Children.Add(CreateStation());
                    break;
                case StationType.TakeoffMiddle:
                    takeoffsMiddlesSP.Children.Add(CreateStation());
                    break;
                case StationType.LandingMiddle:
                    landingsMiddlesSP.Children.Add(CreateStation());
                    break;
                case StationType.LandingsRunway:
                    landingsRunwaysSP.Children.Add(CreateStation());
                    break;
                case StationType.TakeoffsRunway:
                    takeoffsRunwaysSP.Children.Add(CreateStation());
                    break;
                default:
                    break;
            }
        }

        private void CreateLayersStackPanels(out StackPanel landingsRunwaysSP, out StackPanel takeoffsRunwaysSP, out StackPanel landingsMiddlesSP, out StackPanel takeoffsMiddlesSP, out StackPanel hangarsSP)
        {
            landingsRunwaysSP = BuildStationLayerSP();
            takeoffsRunwaysSP = BuildStationLayerSP();
            landingsMiddlesSP = BuildStationLayerSP();
            takeoffsMiddlesSP = BuildStationLayerSP();
            hangarsSP = BuildStationLayerSP();
        }

        private void SetRowsMainGrid()
        {
            for (int i = 0; i < 3; i++)
                MainGrid.RowDefinitions.Add(new RowDefinition());
        }

        private Grid SetGridColumns(int columns)
        {
            Grid grid = new Grid();
            for (int i = 0; i < columns; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            return grid;
        }

        private StationUC CreateStation()
        {
            StationUC station = new StationUC();
            station.Width = 160;
            station.Height = 220;
            station.Margin = new Thickness(15);
            return station;
        }

        private void SendStation(int stationCounter, IStation station) => MessengerInstance.Send(station, $"Station{stationCounter}");

        private StackPanel BuildStationLayerSP()
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            return stackPanel;
        }

        private void AddUIElementToGrid(UIElement element, Grid grid, int column, int row)
        {
            Grid.SetColumn(element, column);
            Grid.SetRow(element, row);
            grid.Children.Add(element);
        }
    }
}
