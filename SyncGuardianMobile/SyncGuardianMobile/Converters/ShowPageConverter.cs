using System;
using System.Globalization;
using Xamarin.Forms;

namespace SyncGuardianMobile.Converters
{
    public class ShowPageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && parameter is string)
            {
                return string.Equals(value as string, parameter as string);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
