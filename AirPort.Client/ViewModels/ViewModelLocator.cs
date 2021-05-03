using AirPort.Client.ViewModels.AirportViewModels;
using AirPort.Client.ViewModels.TablesViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AirPort.Client.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel Main => App.Provider.GetRequiredService<MainViewModel>();
        public StationsTableViewModel Stations => App.Provider.GetRequiredService<StationsTableViewModel>();
        public LandingWaitersViewModel LandingWaiters => App.Provider.GetRequiredService<LandingWaitersViewModel>();
        public TakeoffWaitersViewModel TakeoffWaiters => App.Provider.GetRequiredService<TakeoffWaitersViewModel>();
        public AirportViewModel Airport => App.Provider.GetRequiredService<AirportViewModel>();
        public StationViewModel Station => App.Provider.GetRequiredService<StationViewModel>();
    }
}
