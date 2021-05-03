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
    public class LandingWaitersViewModel : ViewModelBase
    {
        private readonly IClientService service;

        public ObservableCollection<Plane> Waiters { get; set; }

        //ctor
        public LandingWaitersViewModel(IClientService service)
        {
            this.service = service;
            InitializeProps();
            SetEventListenets();
        }

        private void SetEventListenets()
        {
            service.NewLandingWaiter += NewLandingWaiter;
            service.NewLanding += RemoveLandigWaiter;
        }

        private void RemoveLandigWaiter(Plane waiterPlane)
        {
            var res = Waiters.FirstOrDefault(w => w.Id == waiterPlane.Id);
            if (res != null) Waiters.Remove(res);
        }

        /// <summary>
        /// Callback function will execute each time new landing waiter plane will create.
        /// </summary>
        /// <param name="waiterPlane"></param>
        private void NewLandingWaiter(Plane waiterPlane)
        {
            if (!Waiters.Any(w=> w.Id == waiterPlane.Id))
                Waiters.Add(waiterPlane);
        }

        private void InitializeProps() => Waiters = new ObservableCollection<Plane>();
    }

}
