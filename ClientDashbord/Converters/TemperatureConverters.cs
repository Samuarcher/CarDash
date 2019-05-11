using System;
using System.Globalization;
using System.Windows.Data;

namespace ClientDashbord.Converters
{
	public class TemperatureConverters : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double doubleValue)
			{
				return Math.Round((doubleValue - 32) / 1.8, 1);
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}