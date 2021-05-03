using AirPort.Common.Enums;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Airport.Design.Converters
{
    public class PlaneToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var plane = value as Plane;
            if (plane != null)
                return Application.Current.FindResource("PlaneIcon");
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
