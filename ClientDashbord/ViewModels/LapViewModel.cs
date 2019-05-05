namespace ClientDashbord.ViewModels
{
	public class LapViewModel
	{
		public int Numero { get; }

		public double Time { get; }

		public double? TimeDelta { get; }

		public int Position { get; }

		public LapViewModel(int numero, double time, int position, double? timeDelta)
		{
			this.Numero = numero;
			this.Time = time;
			this.Position = position;
			this.TimeDelta = timeDelta;
		}
	}
}