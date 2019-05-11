using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ClientDashbord.Converters
{
	public class TimeConverters : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			TimeSpan time = TimeSpan.Zero;
			bool isNegatif = false;
			if (value is double doubleValue)
			{
				isNegatif = doubleValue < 0;
				if (isNegatif)
				{
					doubleValue = doubleValue * -1;
				}

				time = TimeSpan.FromSeconds(doubleValue);
			}

			if (value is int intValue)
			{
				isNegatif = intValue < 0;
				if (isNegatif)
				{
					intValue = intValue * -1;
				}

				time = TimeSpan.FromMilliseconds(intValue / 1000);
			}

			StringBuilder sb = new StringBuilder();
			if (isNegatif)
			{
				sb.Append("- ");
			}

			if (time.Hours != 0)
			{
				sb.Append($"{time.Hours:00}:");
			}

			if (time.Minutes != 0)
			{
				sb.Append($"{time.Minutes:00}:");
			}

			sb.Append($"{time.Seconds:00}.{time.Milliseconds:000}");

			return sb.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}