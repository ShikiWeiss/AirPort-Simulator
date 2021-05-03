using Airport.Dal.Api;
using AirPort.Common;
using AirPort.Common.Models;
using AirPort.Common.Models.Api;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.Dal
{
    public class AirportRepository : IUnitRepositories
    {
        public IRepository<PlaneToStationMovementDTO> MovementHistoryRepo { get; private set; }
        public IRepository<LandingDTO> LandingsRepo { get; private set; }
        public IRepository<TakeoffDTO> TakeoffsRepo { get; private set; }
        public IRepository<Plane> PlanesRepo { get; private set; }        

        public AirportRepository(
            IRepository<Plane> planesRepo,
            IRepository<PlaneToStationMovementDTO> historyRepo,
            IRepository<LandingDTO> landingRepo,
            IRepository<TakeoffDTO> takeoffRepo)
        {
            MovementHistoryRepo = historyRepo;
            PlanesRepo = planesRepo;
            LandingsRepo = landingRepo;
            TakeoffsRepo = takeoffRepo;
        }
    }
}
