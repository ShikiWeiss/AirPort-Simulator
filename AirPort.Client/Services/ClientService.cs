using AirPort.Common.Enums;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using AirPort.Services.ClientServices.Api;
using AirPort.Services.ServerServices.Api;
using Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AirPort.Services.ClientServices.Implementations
{
    public class ClientService : IClientService
    {
        private HubConnection hubConnection;

        private readonly ISerializer jsonSerializerService;
        #region Events
        public event Action<Plane> NewLandingWaiter;
        public event Action<Plane> NewTakeoffWaiter;
        public event Action<IStation> TakeoffCompleted;

        public event Action<Plane> NewLanding;
        public event Action<Plane> NewTakeoff;
        public event Action<Plane> PlaneMoved;
        #endregion

        public ClientService(ISerializer jsonSerializerService)
        {
            this.jsonSerializerService = jsonSerializerService;
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:62683/AirPortHub")
                .WithAutomaticReconnect()
                .Build();

            SetListenners();
        }

        public async void Connect() => await hubConnection.StartAsync();

        private IEnumerable<Plane> PlanesDesirialize(string json) => jsonSerializerService.Deserialize<IEnumerable<Plane>>(json);
        private Plane PlaneDesirialize(string json) => jsonSerializerService.Deserialize<Plane>(json);

        private void SetListenners()
        {
            hubConnection.On<string>("PlaneMove", (data) =>
             {
                 Dispatcher.CurrentDispatcher.BeginInvoke(() => PlaneMoved?.Invoke(PlaneDesirialize(data)));
             });

            hubConnection.On<string>("TakeoffWaiter", (data) =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(() => NewTakeoffWaiter?.Invoke(PlaneDesirialize(data)));
            });

            hubConnection.On<string>("LandingWaiter", (data) =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(() => NewLandingWaiter?.Invoke(PlaneDesirialize(data)));
            });

            hubConnection.On<string>("Takoff", (data) =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(() => NewTakeoff?.Invoke(PlaneDesirialize(data)));
            });

            hubConnection.On<string>("NewLanding", (data) =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(() => NewLanding?.Invoke(PlaneDesirialize(data)));
            });

            hubConnection.On<string>("TakeoffCompleted", (data) => {
                Dispatcher.CurrentDispatcher.BeginInvoke(()=> TakeoffCompleted?.Invoke(jsonSerializerService.Deserialize<Station>(data)));
            } );
        }

        public async Task<IEnumerable<Plane>> GetLandingWaiters()
        {
            string res = await hubConnection.InvokeAsync<string>(nameof(GetLandingWaiters));
            return PlanesDesirialize(res);
        }

        public async Task<IEnumerable<IStation>> GetStations()
        {
            string res = await hubConnection.InvokeAsync<string>(nameof(GetStations));
            return jsonSerializerService.Deserialize<IEnumerable<Station>>(res);
        }

        public async Task<IEnumerable<Plane>> GetTakeoffWaiters()
        {
            string res = await hubConnection.InvokeAsync<string>(nameof(GetTakeoffWaiters));
            return PlanesDesirialize(res);
        }

    }
}
