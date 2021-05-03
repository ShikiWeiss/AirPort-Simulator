using Airport.Dal.Api;
using AirPort.Common;
using AirPort.Common.Models;
using AirPort.Common.Models.Api;
using AirPort.Services.ServerServices.Api;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AirPort.Services.ServerServices.Implementations
{
    public class DbSaveService : IDbSaveService
    {
        private readonly IUnitRepositories airportRepository;

        //ctor
        public DbSaveService(IUnitRepositories airportRepository) => this.airportRepository = airportRepository;

        public bool AddLanding(ILanding landing) => airportRepository.LandingsRepo.Add(new LandingDTO(landing as Landing));

        public bool AddMovementHistory(PlaneToStationMovement movement) => airportRepository.MovementHistoryRepo.Add(new PlaneToStationMovementDTO(movement));
        public bool AddPlane(Plane plane) => airportRepository.PlanesRepo.Add(plane);

        public bool AddTakeoof(ITakeoff takeoff) => airportRepository.TakeoffsRepo.Add(new TakeoffDTO(takeoff as Takeoff));
    }
}
