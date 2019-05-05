using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;

namespace ClientDashbord.ViewModels
{
	public class RaceViewModel : BaseCarDashViewModel
	{
		private int _lap;

		[PositionByte(280)]
		public double DistanceParcouru { get; set; }
		public double DistanceParcouruKm => this.DistanceParcouru / 1000;

		[PositionByte(300, 2)]
		public int Lap
		{
			get => this._lap;
			set
			{
				if (value != 0 && value + 1 != this.Lap)
				{
					this.ActionPostUpdate.Add(this.NewLap);
				}
				this._lap = value + 1;
			}
		}

		[PositionByte(302, 1)]
		public int Position { get; set; }

		[PositionByte(292)]
		public double TimeLap { get; set; }

		[PositionByte(296)]
		public double TotalTime { get; set; }

		[PositionByte(284)]
		public double BestTimeLap { get; set; }

		[PositionByte(288)]
		public double LastTimeLap { get; set; }

		public ObservableCollection<LapViewModel> Laps { get; set; }

		public RaceViewModel()
		{
			this.Laps = new ObservableCollection<LapViewModel>();
		}

		public void NewLap()
		{
			LapViewModel lastLapViewModel = this.Laps.OrderByDescending(o => o.Numero).FirstOrDefault();
			double? timeDelta = lastLapViewModel != null ? this.LastTimeLap - lastLapViewModel.Time : (double?)null;
			this.Laps.Add(new LapViewModel(this.Lap - 1,
				this.LastTimeLap,
				this.Position,
				timeDelta));
		}

		public void EndRace()
		{
			LapViewModel lastLapViewModel = this.Laps.OrderByDescending(o => o.Numero).FirstOrDefault();
			double? timeDelta = lastLapViewModel != null ? this.TimeLap - lastLapViewModel.Time : (double?)null;
			this.Laps.Add(new LapViewModel(this.Lap,
				this.TimeLap,
				this.Position,
				timeDelta));
		}
	}
}