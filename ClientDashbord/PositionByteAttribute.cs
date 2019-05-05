using System;

namespace ClientDashbord
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PositionByteAttribute : Attribute
	{
		public int Start { get; }

		public int Length { get; }

		public PositionByteAttribute(int start)
			: this(start, 4)
		{
		}

		public PositionByteAttribute(int start, int length)
		{
			this.Start = start;
			this.Length = length;
		}
	}
}