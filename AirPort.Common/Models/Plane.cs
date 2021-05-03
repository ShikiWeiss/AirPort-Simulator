using AirPort.Common.Models.Stations;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Common.Models
{
    [Serializable]
    public class Plane : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Company { get; set; }

        public int CurrentStationId { get; set; }

        public bool IsLanded { get; set; }

        [NotMapped]
        public string ColorName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}