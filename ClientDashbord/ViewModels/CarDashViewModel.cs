namespace ClientDashbord.ViewModels
{
	public class CarDashViewModel
	{
		public CarInformationViewModel CarInformationViewModel { get; }

		public RaceViewModel RaceViewModel { get; }

		public string CircuitName { get; set; }

		public string CircuitVersion { get; set; }

		public CarDashViewModel()
		{
			this.CarInformationViewModel = new CarInformationViewModel();
			this.RaceViewModel = new RaceViewModel();
		}

		public void Update(byte[] bytes)
		{
			this.CarInformationViewModel.Update(bytes);
			this.RaceViewModel.Update(bytes);
		}

		public void EndRace()
		{
			this.RaceViewModel.EndRace();
		}
	}
}