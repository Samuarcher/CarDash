using System;
using System.Collections.ObjectModel;

namespace ClientDashbord.ViewModels
{
	public class CarDashViewModel : BaseCarDashViewModel
	{
		private int _time;

		public HistoriquesViewModel HistoriqueViewModels { get; set; }

		public CarInformationViewModel CarInformationViewModel { get; }

		public RaceViewModel RaceViewModel { get; }

		[PositionByte(4)]
		public int Time
		{
			get => this._time;
			set
			{
				this._time = value;
				this.ActionPostUpdate.Add(this.NewHistorique);
			}
		}

		public string CircuitName { get; set; }

		public string CircuitVersion { get; set; }

		public string TabName => this.DateStartRace.ToString("G");

		public DateTime DateStartRace { get; set; }

		public CarDashViewModel()
		{
			this.DateStartRace = DateTime.Now;
			this.HistoriqueViewModels = new HistoriquesViewModel();
			this.CarInformationViewModel = new CarInformationViewModel();
			this.RaceViewModel = new RaceViewModel();
		}

		public override void Update(byte[] bytes)
		{
			this.CarInformationViewModel.Update(bytes);
			this.RaceViewModel.Update(bytes);
			base.Update(bytes);
		}

		public void EndRace()
		{
			this.RaceViewModel.EndRace();
			this.CarInformationViewModel.EndRace();
		}

		private void NewHistorique()
		{
			HistoriqueViewModel historiqueViewModel = new HistoriqueViewModel
			{
				Time = this.RaceViewModel.TotalTime,
				Vitesse = this.CarInformationViewModel.Vitesse,
				Acceleration = this.CarInformationViewModel.Acceleration,
				Frein = this.CarInformationViewModel.Frein,
				Fuel = this.CarInformationViewModel.Fuel,
				TempPneuFL = this.CarInformationViewModel.TempPneuFL,
				TempPneuFR = this.CarInformationViewModel.TempPneuFR,
				TempPneuRL = this.CarInformationViewModel.TempPneuRL,
				TempPneuRR = this.CarInformationViewModel.TempPneuRR,
			};
			this.HistoriqueViewModels.Add(historiqueViewModel);
		}
	}
}