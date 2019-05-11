using System;
using System.Globalization;
using System.Windows.Data;

namespace ClientDashbord.Converters
{
	public class OctetsConverters : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is long longValue)
			{
				return this.ConvertOctet(longValue);
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private string ConvertOctet(long octets)
		{
			string[] unit = { "o", "Ko", "Mo", "Go", "To" };
			string str = "";
			long[] detail = new long[5];
			int i = 0;
			do
			{
				detail[i] = octets % 1024;
				octets /= 1024;
				i++;
			} while (octets >= 1024 && i < 5);
			detail[i] = octets;
			for (int k = detail.Length - 1; k >= 0; k--)
			{
				if (detail[k] != 0)
				{
					str += (detail[k] + " " + unit[k] + " ");
				}
			}
			return str;
		}
	}
}