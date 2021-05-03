using AirPort.Common;
using AirPort.Common.Models;
using AirPort.Common.Models.Api;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.Dal.Api
{
    public interface IUnitRepositories
    {
        IRepository<PlaneToStationMovementDTO> MovementHistoryRepo { get; }
        IRepository<LandingDTO> LandingsRepo { get; }
        IRepository<TakeoffDTO> TakeoffsRepo { get; }
        IRepository<Plane> PlanesRepo { get; }
    }
}
