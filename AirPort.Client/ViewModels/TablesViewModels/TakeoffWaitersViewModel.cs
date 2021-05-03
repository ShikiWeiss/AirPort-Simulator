using AirPort.Services.ClientServices.Api;
using Common.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AirPort.Client.ViewModels.TablesViewModels
{
    public class TakeoffWaitersViewModel : ViewModelBase
    {
        private readonly IClientService service;

        public ObservableCollection<Plane> Waiters { get; set; }

        //ctor
        public TakeoffWaitersViewModel(IClientService service)
        {
            this.service = service;
            InitializeProps();
            SetEventListeners();
        }

        /// <summary>
        /// Method register to events from hub. 
        /// </summary>
        private void SetEventListeners()
        {
            service.NewTakeoffWaiter += NewTakeoffWaiter;
            service.NewTakeoff += RemoveWaiterPlane;
            service.PlaneMoved += PlaneMoved;
        }

        private void PlaneMoved(Plane plane)
        {
            var res = Waiters.FirstOrDefault(p => p.Id == plane.Id);
            if (res != null)
            {
                //res.CurrentStationId = plane.CurrentStationId;
                int index = Waiters.IndexOf(res);
                Waiters.RemoveAt(index);
                Waiters.Insert(index, plane);
            }

        }

        /// <summary>
        /// Callback function will execute each time palne will takeoff.
        /// Thid method should check if this plane waited befor, if he is he would remove from waiters list.
        /// </summary>
        /// <param name="waiterPlane"></param>
        private void RemoveWaiterPlane(Plane waiterPlane)
        {
            var res = Waiters.FirstOrDefault(w => w.Id == waiterPlane.Id);
            if (res != null) Waiters.Remove(res);
        }

        /// <summary>
        /// Callback function will execute each time new takeoff waiter plane will create.
        /// </summary>
        /// <param name="obj"></param>
        private void NewTakeoffWaiter(Plane waiterPlane)
        {
            if (!Waiters.Any(w => w.Id == waiterPlane.Id))
                Waiters.Add(waiterPlane);
        }

        private void InitializeProps() => Waiters = new ObservableCollection<Plane>();//service.GetTakeoffWaiters().Result);

    }
}
