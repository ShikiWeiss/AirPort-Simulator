using AirPort.Common.Enums;
using AirPort.Common.Models.AirportActions;
using AirPort.Server.BL.Api;
using AirPort.Services.ServerServices.Api;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AirPort.Server.Hubs
{
    public class AirPortHub : Hub
    {
        private readonly IHubContext<AirPortHub> context;
        private readonly IDataService dataService;
        private readonly IGenerator generator;

        //ctor
        public AirPortHub(IHubContext<AirPortHub> context, IDataService dataService, IGenerator generator)
        {
            SetListeners();
            this.context = context;
            this.dataService = dataService;
            this.generator = generator;
        }

        /// <summary>
        /// Private method for setting callback function to Airport events.
        /// </summary>
        private void SetListeners()
        {
            AirportActions.LandingAction += NewLanding;
            AirportActions.TakeoffAction += Takoff;
            AirportActions.LandingWaiterAction += LandingWaiter;
            AirportActions.TakeoffWaiterAction += TakeoffWaiter;
            AirportActions.PlaneMovedAction += PlaneMove;
            AirportActions.TakeoffCompletedAction += TakeoffCompleted;
        }

        //hub to client
        /// <summary>
        /// Callback functions for AirportActions invoke event.
        /// Should notify client abount changes.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="action"></param>
        private void PlaneMove(string data) => context.Clients.All.SendAsync(nameof(PlaneMove), data);

        private void TakeoffWaiter(string data) => context.Clients.All.SendAsync(nameof(TakeoffWaiter), data);

        private void LandingWaiter(string data) => context.Clients.All.SendAsync(nameof(LandingWaiter), data);

        private void Takoff(string data) => context.Clients.All.SendAsync(nameof(Takoff), data);

        private void NewLanding(string data) => context.Clients.All.SendAsync(nameof(NewLanding), data);

        private void TakeoffCompleted(string data) => context.Clients.All.SendAsync(nameof(TakeoffCompleted), data);


        //client to hub
        public async Task<string> GetLandingWaiters() => await dataService.GetLandingWaitersJson();

        public async Task<string> GetStations() => await dataService.GetStationsJson();

        public async Task<string> GetTakeoffWaiters() => await dataService.GetTakeoffWaitersJson();
    }
}
