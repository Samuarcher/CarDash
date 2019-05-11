using System;
using System.Globalization;
using System.Windows.Data;

namespace ClientDashbord.Converters
{
	public class PourcentageConverters : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			long? max = null;
			if (parameter != null)
			{
				max = System.Convert.ToInt64(parameter);
				if (max == 0) return 0;
			}

			if (value is double doubleValue)
			{
				return max.HasValue ? Math.Round(doubleValue * 100 / max.Value, 2) : Math.Round(doubleValue * 100, 2);
			}

			if (value is int intValue)
			{
				double valeur = max.HasValue ? intValue * 100 / max.Value : intValue * 100;
				return Math.Round(valeur, 2);
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}