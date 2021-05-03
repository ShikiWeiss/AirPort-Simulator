using AirPort.Common.Enums;
using AirPort.Common.Models.AirportActions;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Services.ServerServices.Api;
using AirPort.Services.ServerServices.Implementations;
using Common.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class NotifyService : INotifyService
    {
        private readonly ISerializer jsonSerializerService;

        //ctor
        public NotifyService(ISerializer jsonSerializerService)
        {
            this.jsonSerializerService = jsonSerializerService;
        }

        private string Serialize(object obj) => jsonSerializerService.Serialize(obj);
        private bool IsNullOrEmpty(string str) => string.IsNullOrEmpty(str);


        public void InvokeLanding(Plane plane)
        {
            if (SerializeCheck(plane, AirportActionsEnum.Landing, out string res))
                AirportActions.LandingAction?.Invoke(res);
        }
        public void InvokeTakeoff(Plane plane)
        {
            if (SerializeCheck(plane, AirportActionsEnum.Takeoff, out string res))
                AirportActions.TakeoffAction?.Invoke(res);
        }
        public void InvokeTakeoffWaiter(Plane plane)
        {
            if (SerializeCheck(plane, AirportActionsEnum.TakeoffWaiter, out string res))
                AirportActions.TakeoffWaiterAction?.Invoke(res);
        }
        public void InvokeLandingWaiter(Plane plane)
        {
            if (SerializeCheck(plane, AirportActionsEnum.LandingWaiter, out string res))
                AirportActions.LandingWaiterAction?.Invoke(res);
        }
        public void InvokePlaneMoved(Plane plane)
        {
            if (SerializeCheck(plane, AirportActionsEnum.PlaneMoved, out string res))
                AirportActions.PlaneMovedAction?.Invoke(res);

        }

        private bool SerializeCheck(Plane plane, AirportActionsEnum action, out string res)
        {
            res = Serialize(plane);
            if (!IsNullOrEmpty(res)) return true;
            return false;
        }

        public void InvokeTakeoffCompleted(IStation runway)
        {
            var res = Serialize(runway);
            if (res != null)
                AirportActions.TakeoffCompletedAction?.Invoke(res);
        }
    }
}
