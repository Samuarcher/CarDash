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
			if (value is double doubleValue)
			{
				bool isNegatif = doubleValue < 0;
				if (isNegatif)
				{
					doubleValue = doubleValue * -1;
				}

				TimeSpan time = TimeSpan.FromSeconds(doubleValue);

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

				sb.Append($"{time.Seconds:00}.{time.Milliseconds}");

				return sb.ToString();
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}