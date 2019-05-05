using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace ClientDashbord
{
	public static class Extension
	{
		private static IDictionary<Type, Func<byte[], object>> _typeConverter = new Dictionary<Type, Func<byte[], object>>
		{
			{typeof(double), (bytes => bytes.ToFloat()) },
			{typeof(int), (bytes => bytes.ToInt()) }
		};

		public static void ExecuteOnUi(Action action)
		{
			Dispatcher dispatchObject = Application.Current.Dispatcher;
			if (dispatchObject == null || dispatchObject.CheckAccess())
			{
				action();
			}
			else
			{
				dispatchObject.Invoke(action);
			}
		}

		public static byte[] GetPartOfByteTab(this byte[] bytes, PositionByteAttribute positionByte)
		{
			byte[] bytesCopie = new byte[positionByte.Length];
			Array.Copy(bytes, positionByte.Start, bytesCopie, 0, positionByte.Length);

			return bytesCopie;
		}

		public static object ConvertByte(this byte[] bytes, Type type)
		{
			return _typeConverter[type](bytes);
		}

		public static double ToFloat(this byte[] bytes)
		{
			return BitConverter.ToSingle(bytes, 0);
		}

		public static int ToInt(this byte[] bytes)
		{
			byte[] byteInt = new byte[4];
			Array.Copy(bytes, byteInt, bytes.Length);

			return BitConverter.ToInt32(byteInt, 0);
		}
	}
}