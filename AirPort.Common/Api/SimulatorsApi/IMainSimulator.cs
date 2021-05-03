using AirPort.Common.Models.Api.StationApi;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirPort.Server.BL.Simulator.Api
{
    public interface IMainSimulator
    {
        /// <summary>
        /// Method should manage landing come from generator.
        /// </summary>
        /// <param name="plane"></param>
        void ManageLandingRequest(Plane plane);

        /// <summary>
        /// Method should manage plane arrival to stations.
        /// </summary>
        /// <param name="station"></param>
        /// <param name="plane"></param>
        void OnStationArrival(IStation station, Plane plane);

        /// <summary>
        /// Method should move plane to next available station.
        /// </summary>
        /// <param name="station"></param>
        /// <param name="plane"></param>
        void MoveToNextStation(IStation station, Plane plane);
    }
}
