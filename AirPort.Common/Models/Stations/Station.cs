using AirPort.Common.Enums;
using AirPort.Common.Models.Api.StationApi;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace AirPort.Common.Models.Stations
{
    [Serializable]
    public class Station : IStation, INotifyPropertyChanged
    {
        public Station() => Locker = new object();

        public int Id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Action<IStation, Plane> Cleared { get; set; }

        private Plane plane;  public Plane CurrentPlane { get { return plane; } set { plane = value; OnPropertyChanged(); } }

        public List<Plane> PlaneWaiters { get; set; }

        [NotMapped]
        public object Locker { get; }

        [NotMapped]
        [JsonIgnore]
        public List<Station> NextStations { get; set; }

        [NotMapped]
        public StationType Type { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string plane = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(plane)));
    }

}
