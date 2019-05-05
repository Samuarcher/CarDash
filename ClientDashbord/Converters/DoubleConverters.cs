using System;
using System.Globalization;
using System.Windows.Data;

namespace ClientDashbord.Converters
{
	public class DoubleConverters : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int nbDecimal = 0;
			if (parameter != null)
			{
				nbDecimal = System.Convert.ToInt32(parameter);
			}

			if (value is double doubleValue)
			{
				return Math.Round(doubleValue, nbDecimal);
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}