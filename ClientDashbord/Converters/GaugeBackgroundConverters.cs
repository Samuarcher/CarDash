using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ClientDashbord.Converters
{
	public class GaugeBackgroundConverters : IValueConverter
	{
		public PourcentageConverters PourcentageConverters { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			value = this.PourcentageConverters?.Convert(value, targetType, parameter, culture);

			if (value is double doubleValue)
			{
				if (doubleValue < 10)
				{
					return Brushes.Red;
				}

				if (doubleValue < 30)
				{
					return Brushes.Orange;
				}

				return Brushes.LimeGreen;
			}

			return Brushes.LimeGreen;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}