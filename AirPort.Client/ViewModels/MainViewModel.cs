using AirPort.Services.ClientServices.Api;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IClientService clientService;

        public MainViewModel(IClientService clientService)
        {
            this.clientService = clientService;
            Connect();
        }

        private void Connect() => clientService.Connect(); //hubConnection
    }
}
