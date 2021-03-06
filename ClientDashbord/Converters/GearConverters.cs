﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace ClientDashbord.Converters
{
	public class GearConverters : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int intValue)
			{
				if (intValue < 0)
				{
					return "N";
				}

				if (intValue == 0)
				{
					return "R";
				}

				return intValue.ToString();
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}