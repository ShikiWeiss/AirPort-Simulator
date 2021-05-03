using AirPort.Server.BL.Api;
using AirPort.Server.BL.Simulator.Api;
using Common.Models;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Timers;

namespace AirPort.Server.BL
{
    public class Generator : IGenerator
    {
        private readonly IMainSimulator simulator;
        private Random rnd;
        private Mocks mocks;
        private int minInterval;
        private int maxInterval;
        private Timer tmr;

        public Generator(IMainSimulator simulator)
        {
            this.simulator = simulator;
            SetProps();
            SetTimer();
        }

        private void SetProps()
        {
            rnd = new Random();
            mocks = new Mocks();
            minInterval = 3000;
            maxInterval = 5000;
        }

        private void SetTimer()
        {
            tmr = new Timer();
            tmr.Interval = rnd.Next(minInterval, maxInterval);
            tmr.Elapsed += Tmr_Elapsed;
            tmr.Start();
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e) => Task.Run(() => simulator.ManageLandingRequest(CreatePlanes()));

        public async void Start() => await Task.Run(() => simulator.ManageLandingRequest(CreatePlanes()));

        private Plane CreatePlanes()
        {
            return new Plane
            {
                IsLanded = false,
                Company = mocks.Companies[rnd.Next(mocks.Companies.Count)],
                ColorName = $"#{Color.FromArgb(rnd.Next(100, 255), rnd.Next(100, 255), rnd.Next(100, 255)).Name}"
            };
        }

    }
}
