using AirPort.Common.Enums;
using AirPort.Common.Models.Api.StationApi;
using Common.Models;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Service should suply the ability to notify other parts of the software about changes.
    /// </summary>
    public interface INotifyService
    {
        void InvokePlaneMoved(Plane plane);
        void InvokeLanding(Plane plane);
        
        void InvokeTakeoff(Plane plane);
        
        void InvokeTakeoffWaiter(Plane plane);
        
        void InvokeLandingWaiter(Plane plane);

        void InvokeTakeoffCompleted(IStation runway);
    }
}
