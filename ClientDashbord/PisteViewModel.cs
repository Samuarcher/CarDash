namespace ClientDashbord
{
	public class PisteViewModel : BaseNotify
	{
		public string NomPiste { get; set; }

		public double Distance { get; set; }

		public PisteViewModel(string nomPiste, double distance)
		{
			this.NomPiste = nomPiste;
			this.Distance = distance;
		}
	}
}