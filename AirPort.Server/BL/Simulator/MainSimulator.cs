using Airport.Utilities.Api;
using AirPort.Common.Enums;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using AirPort.Server.BL.Simulator.Api;
using AirPort.Services.ServerServices.Api;
using Common.Enums;
using Common.Models;
using Common.Models.Stations;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirPort.Server.BL.Simulator
{
    public class MainSimulator : IMainSimulator
    {
        #region global variables
        private object locker = new object();

        private readonly ISimulatorService simulatorService;
        private readonly ILandingSimulator landingSimulator;
        private readonly ITakeoffSimulator takeoffSimulator;
        private readonly IDataService dataService;
        private readonly IDbSaveService dbSaveService;

        public List<LandingRunwayStation> LandingRunways { get; set; }
        public List<TakeoffRunwayStation> TakeoffRunways { get; set; }
        public List<Plane> LandingWaiters { get; set; }
        public List<Plane> TakeoffWaiters { get; set; }

        #endregion

        //ctor
        public MainSimulator(ISimulatorService simulatorService, ILandingSimulator landingSimulator, ITakeoffSimulator takeoffSimulator, IDataService dataService, IDbSaveService dbSaveService)
        {
            this.simulatorService = simulatorService;
            this.landingSimulator = landingSimulator;
            this.takeoffSimulator = takeoffSimulator;
            this.dataService = dataService;
            this.dbSaveService = dbSaveService;
            SetProps();
        }

        private async void SetProps()
        {
            LandingRunways = dataService.GetLandingRunways();
            TakeoffRunways = dataService.GetTakeoffRunways();

            LandingWaiters = await dataService.GetLandingWaiters();
            TakeoffWaiters = await dataService.GetTakeoffWaiters();
        }

        private LandingRunwayStation GetRunwayToLand() => LandingRunways.FirstOrDefault(r => r.CurrentPlane == null);
        private bool IsThereLandingWaiter() => LandingWaiters?.Count > 0;
        private void AddTakeoffWaiterToList(Plane plane) => TakeoffWaiters.Add(plane);
        public void MoveToNextStation(IStation station, Plane plane) => simulatorService.MoveToNextStation(station, plane);
        private bool IsCanLand(out LandingRunwayStation rws)
        {
            rws = GetRunwayToLand();
            return rws != null;
        }

        public void ManageLandingRequest(Plane plane)
        {
            lock (locker)
            {
                dbSaveService.AddPlane(plane);
                if (IsAvailableRunway(out LandingRunwayStation rws)) //check for available runway
                {
                    Land(plane, rws);                                //Land by landing simulator.
                    Task.Run(() => ContinueMovement(plane, rws));    //keep move on route.
                }
                else AddNewLandingWaiter(plane);                     // no available runway. add plane to waiters.
            }
        }

        private void AddNewLandingWaiter(Plane plane)
        {
            LandingWaiters.Add(plane);

            simulatorService.InvokeLandingWaiter(plane);

            simulatorService.KeepPlaneWait(plane, LandingRunways, LandAfterWaiting);
        }

        private void LandAfterWaiting(IStation station, Plane plane)
        {
            lock (station.Locker)
            {
                if (IsFirstWaiterPlane(station, plane)) //check if its thread first time.
                {
                    simulatorService.RemoveWaiterFromStations(plane, LandingRunways.Cast<IStation>()); //remove plane from station waiters list.
                    LandingRunwayStation rws = station as LandingRunwayStation;
                    Land(plane, rws);
                    LandingWaiters.Remove(plane);
                    Task.Run(() => ContinueMovement(plane, rws)); //Run it in another thread so other threads will execute.
                }
            }
        }

        public void OnStationArrival(IStation station, Plane plane)
        {
            if (station is HangarStation hangar)// in hangar 
                OnHangarArrival(plane, hangar);
            else if (station is TakeoffRunwayStation runway) // in runway
                OnRunwayArrival(plane, runway);
            else
                OnMiddleArrival(station, plane);
        }

        private void OnMiddleArrival(IStation station, Plane plane) => ContinueMovement(plane, station);

        private void OnRunwayArrival(Plane plane, TakeoffRunwayStation runway) => takeoffSimulator.Takeoff(plane, runway);

        private void OnHangarArrival(Plane plane, HangarStation hangar)
        {
            AddTakeoffWaiterToList(plane);
            simulatorService.TimeToSleep(10000); //time for plane maintenance.
            simulatorService.InvokeTakeoffWaiter(plane);
            ContinueMovement(plane, hangar);
        }

        private void ContinueMovement(Plane plane, IStation currentStation)
        {
            IStation nextStation = simulatorService.GetAvailabelNextStation(currentStation);
            if (nextStation != null)
            {
                ExitedStationOperations(plane);
                simulatorService.SetPlaneEnteredTime(plane, nextStation);   //update new station enterance time
                MoveToNextStation(nextStation, plane);                      //move plane
                ClearStationPlane(currentStation);                          //clear station
                simulatorService.InvokeStationCleared(currentStation);      //notify listeners station cleared
                OnStationArrival(nextStation, plane);                       //Operations after plane move to new station.
            }
            else
            {
                simulatorService.KeepPlaneWait(plane, currentStation.NextStations, OnNextStationCleared);
                // if (currentStation is TakeoffRunwayStation) simulatorService.InvokeTakeoffWaiter(plane);
            }
        }

        private void ExitedStationOperations(Plane plane)
        {
            SetPlaneExitedTime(plane);
            DbMovementHistoryUpdate(simulatorService.GetPlaneHistory());  //update old station
        }

        private void SetPlaneExitedTime(Plane plane) => simulatorService.SetPlaneExitedTime(plane);

        private void DbMovementHistoryUpdate(PlaneToStationMovement psm) => dbSaveService.AddMovementHistory(psm);

        private void ClearStationPlane(IStation station) => station.CurrentPlane = null;

        private void Land(Plane plane, LandingRunwayStation rws) => landingSimulator.Land(plane, rws);

        /// <summary>
        /// Private method looking for available landing runway.
        /// </summary>
        /// <returns></returns>
        private bool IsAvailableRunway(out LandingRunwayStation rws)
        {

            rws = default;
            return (!IsThereLandingWaiter()) && IsCanLand(out rws);

        }

        /// <summary>
        /// Callback method for station cleared event.
        /// </summary>
        /// <param name="station"></param>
        /// <param name="plane"></param>
        private void OnNextStationCleared(IStation station, Plane plane)
        {
            lock (station.Locker)
            {
                if (IsFirstWaiterPlane(station, plane))
                {
                    RemovePlaneFromStationsWaiters(plane);
                    ClearStationPlane(station);         //clear station
                    if (station.Type == StationType.TakeoffsRunway) Task.Run(() => OnStationArrival(station, plane));
                    else Task.Run(() => ContinueMovement(plane, station));
                }
            }
        }
        private bool IsFirstWaiterPlane(IStation station, Plane plane)
        {
            if (plane != null)
            {
                Plane waiterPlane = station.PlaneWaiters.FirstOrDefault();//get first waiter plane.
                return plane.Equals(waiterPlane);
            }
            return false;
        }
        private void RemovePlaneFromStationsWaiters(Plane plane)
        {
            Station s = dataService.GetStationById(plane.CurrentStationId);
            if (s != null && !(s is TakeoffRunwayStation))
                simulatorService.RemoveWaiterFromStations(plane, s.NextStations);
        }
    }
}
