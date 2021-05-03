using AirPort.Server.BL.Api;
using AirPort.Server.BL.Simulator.Api;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AirPort.Server.BL
{
    public class Generator : IGenerator
    {
        private readonly ISimulatorManager simulator;
        private Random rnd;
        private Mocks Mocks;
        private int minInterval;
        private int maxInterval;
        private System.Timers.Timer tmr;

        public Generator(ISimulatorManager simulator)
        {
            this.simulator = simulator;
            SetProps();
            SetTimer();
        }

        private void SetProps()
        {
            rnd = new Random();
            Mocks = new Mocks();
            minInterval = 1500;
            maxInterval = 4000;
        }

        private void SetTimer()
        {
            tmr = new System.Timers.Timer();
            tmr.Interval = rnd.Next(minInterval, maxInterval);
            tmr.Elapsed += Tmr_Elapsed;
            tmr.Start();
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e) => Task.Run(() => simulator.ManageLandingRequest(CreatePlanes()));

        public async void Start() => await Task.Run(() =>simulator.ManageLandingRequest(CreatePlanes()));

        private Plane CreatePlanes()
        {
            return new Plane
            {
                IsLanded = false,
                Company = Mocks.Companies[rnd.Next(Mocks.Companies.Count)],
                ColorName = $"#{Color.FromArgb(rnd.Next(100, 255), rnd.Next(100, 255), rnd.Next(100, 255)).Name}"
            };
        }

    }
}
