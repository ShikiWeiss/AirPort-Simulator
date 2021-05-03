using AirPort.Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Airport.Design.Converters
{
    class StationTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is StationType type)
            {
                switch (type)
                {
                    case StationType.Hangar:
                        return "LightSeaGreen";
                    case StationType.TakeoffMiddle:
                    case StationType.LandingMiddle:
                        return "LightBlue";
                    case StationType.LandingsRunway:
                    case StationType.TakeoffsRunway:
                        return "LightSlateGray";
                    default:
                        break;
                }
            }
            return "white";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
