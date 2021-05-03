using Common.Enums;
using Common.Models;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace AirportServer.BL
{
    public class Generator
    {
        private readonly Simulator simulator;
        private Random random;
        public Generator(Simulator simulator)
        {
            this.simulator = simulator;
            random = new Random();
        }

        

        public void CreatePlanes()
        {
            while (true)
            {
                Thread.Sleep(random.Next(2000, 5000));
                Task.Run(() => simulator.NewLandingRequest(new Plane 
                { 
                    FlightAction = FlightActionsEnum.Landing,
                    Color = Color.FromArgb(random.Next(1,255),
                    random.Next(1, 255), random.Next(1, 255))
                }));
            }
        }
    }
}
