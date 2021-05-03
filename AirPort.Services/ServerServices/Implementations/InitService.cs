using AirPort.Common.Enums;
using AirPort.Common.Models;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using AirPort.Services.ServerServices.Api;
using Common.Enums;
using Common.Models;
using Common.Models.Stations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirPort.Services.ServerServices.Implementations
{
    public class InitService : IInitService
    {

        public InitService()
        {
        }

        private List<Station> SetStations()
        {

            TakeoffRunwayStation tr1 = new TakeoffRunwayStation { Id = 15, NextStations = null, PlaneWaiters = new List<Plane>(), Type = StationType.TakeoffsRunway };
            TakeoffRunwayStation tr2 = new TakeoffRunwayStation { Id = 16, NextStations = null, PlaneWaiters = new List<Plane>(), Type = StationType.TakeoffsRunway };

            MiddleStation tms1 = new MiddleStation { Id = 13, NextStations = new List<Station> { tr1, tr2 }, PlaneWaiters = new List<Plane>(), Type = StationType.TakeoffMiddle };
            MiddleStation tms2 = new MiddleStation { Id = 14, NextStations = new List<Station> { tr1, tr2 }, PlaneWaiters = new List<Plane>(), Type = StationType.TakeoffMiddle };

            HangarStation hs1 = new HangarStation { Id = 5, NextStations = new List<Station> { tms1, tms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.Hangar };
            HangarStation hs2 = new HangarStation { Id = 6, NextStations = new List<Station> { tms1, tms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.Hangar };
            HangarStation hs3 = new HangarStation { Id = 7, NextStations = new List<Station> { tms1, tms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.Hangar };
            HangarStation hs4 = new HangarStation { Id = 8, NextStations = new List<Station> { tms1, tms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.Hangar };
            HangarStation hs5 = new HangarStation { Id = 9, NextStations = new List<Station> { tms1, tms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.Hangar };
            HangarStation hs6 = new HangarStation { Id = 10, NextStations = new List<Station> { tms1, tms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.Hangar };
            HangarStation hs7 = new HangarStation { Id = 11, NextStations = new List<Station> { tms1, tms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.Hangar };
            HangarStation hs8 = new HangarStation { Id = 12, NextStations = new List<Station> { tms1, tms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.Hangar };

            MiddleStation lms1 = new MiddleStation { Id = 3, NextStations = new List<Station> { hs1, hs2, hs3, hs4 ,hs5,hs6,hs7,hs8}, PlaneWaiters = new List<Plane>(), Type = StationType.LandingMiddle };                                                                                     
            MiddleStation lms2 = new MiddleStation { Id = 4, NextStations = new List<Station> { hs1, hs2, hs3, hs4,hs5,hs6,hs7,hs8 }, PlaneWaiters = new List<Plane>(), Type = StationType.LandingMiddle };

            LandingRunwayStation lrs1 = new LandingRunwayStation { Id = 1, NextStations = new List<Station> { lms1,lms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.LandingsRunway };
            LandingRunwayStation lrs2 = new LandingRunwayStation { Id = 2, NextStations = new List<Station> { lms1,lms2 }, PlaneWaiters = new List<Plane>(), Type = StationType.LandingsRunway };



            List<Station> stations = new List<Station> { lrs1, lrs2, lms1, lms2, hs1, hs2, hs3, hs4, hs5, hs6, hs7, hs8, tms1, tms2, tr1, tr2 };


            return stations;
        }

        public List<Plane> GetLandingWaiters() => new List<Plane>();

        public List<Station> GetStations() => SetStations();

        public List<Plane> GetTakeoffWaiters() => new List<Plane>();
    }
}
