using AirportServer.Dal.Repository;
using AirportServer.Hubs;
using Common.Enums;
using Common.Models;
using Common.Models.Stations;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirportServer.BL
{
    public class Simulator
    {
        private readonly INotifyService notifyService;

        public List<RunwayStation> LandingRunways { get; set; }
        public List<RunwayStation> TakeoffRunways { get; set; }
        public List<Plane> LandingWaiters { get; set; }
        public List<Plane> TakeoffWaiters { get; set; }
        public Simulator(INotifyService notifyService)
        {
            this.notifyService = notifyService;
        }

        //Plane arrive to the airport's airspace and request landing.
        public void NewLandingRequest(Plane plane)
        {
            if (!IsThereLandingWaiter())
            {
                RunwayStation runway = GetRunwayToLand();
                if (runway != null)
                {
                    Land(plane, runway);
                    return;
                }
            }
            LandingWaiters.Add(plane);
            notifyService.NewLandingWaiter(plane);
            //db update
        }

        private void Land(Plane plane, RunwayStation runway)
        {
            plane.CurrentStation = runway;
            runway.CurrentPlane = plane;
            //ui update
            Task.Run(() => notifyService.NewLanding(plane));
            Thread.Sleep(2000);
            //db update
            Flight flight = CreateFlightAction(plane, runway, FlightActionsEnum.Landing);
            //!!!!
            MiddleStation station = GetNextStation(runway) as MiddleStation;
            if (station != null)
            {
                MoveToNextStation(station, plane);
                runway.CurrentPlane = null;
            }
        }

        public Flight CreateFlightAction(Plane plane, RunwayStation runway, FlightActionsEnum flightAction)
        {
            return new Flight
            {
                Plane = plane,
                RunwayStation = runway,
                Time = DateTime.Now,
                FlightAction = flightAction
            };
        }

        private bool IsThereLandingWaiter() => LandingWaiters.Count > 0;


        private RunwayStation GetRunwayToLand()
        {
            //All the logic of when and where to land goes here 
            return LandingRunways.FirstOrDefault(r => r.CurrentPlane == null);
        }

        public void MoveToNextStation(Station station, Plane plane)
        {
            plane.CurrentStation = station;
            station.CurrentPlane = plane;
            Thread.Sleep(2000);
            Task.Run(() => notifyService.PlaneMoved(plane));
            //db update!!!!!!
            if (station is HangarStation hangar)// in hangar 
                OnHangarArrival(plane, hangar);
            else if (station is RunwayStation runway) // in runway
                OnRunwayArrival(plane, runway);
            else // in middle station
            {
                Station nextStation = GetNextStation(station);
                if (station != null) // there is station to move to
                {
                    MoveToNextStation(nextStation, plane);
                    station.CurrentPlane = null;
                }
            }

        }

        private bool CanMoveToStation(Station NextStation, FlightActionsEnum flightAction)
        {
            return true;
        }

        public Station GetNextStation(Station CurrentStation)
        {
            return CurrentStation.NextStations.FirstOrDefault(s => s.CurrentPlane == null);
        }

        private void OnHangarArrival(Plane plane, HangarStation station)
        {
            Thread.Sleep(15000);
            plane.FlightAction = FlightActionsEnum.Takeoff;
            TakeoffWaiters.Add(plane);
            Task.Run(() => notifyService.NewTakeoffWaiter(plane));
            MiddleStation nextStation = GetNextStation(station) as MiddleStation;
            if(nextStation != null)
            {
                MoveToNextStation(nextStation, plane);
                station.CurrentPlane = null;
            }

        }

        private void OnRunwayArrival(Plane plane, RunwayStation runway)
        {
            Task.Run(() => notifyService.NewTakeoff(plane));
            //db update!!!!!
            /*Task.Run(() =>*/ Flight flight = CreateFlightAction(plane, runway, plane.FlightAction);
            plane = null;
            runway.CurrentPlane = null;
        }
    }
}
