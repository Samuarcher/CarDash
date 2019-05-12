namespace ClientDashbord
{
	public class PisteViewModel : BaseNotify
	{
		public string NomPiste { get; set; }

		public PisteViewModel(string nomPiste)
		{
			this.NomPiste = nomPiste;
		}
	}
}