using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ClientDashbord.ViewModels
{
	public class CarInformationViewModel : BaseCarDashViewModel
	{
		[PositionByte(303, 1)]
		public int Acceleration { get; set; }

		[PositionByte(304, 1)]
		public int Frein { get; set; }

		[PositionByte(306, 1)]
		public int FreinAMain { get; set; }


		[PositionByte(244)]
		public double Vitesse { get; set; }
		public List<double> Vitesses { get; set; }
		public double VitesseMoyenne => this.Vitesses.Sum() / this.Vitesses.Count;
		public double VitesseMax => this.Vitesses.Any() ? this.Vitesses.Max() : 0;


		[PositionByte(12)]
		public double RpmIdle { get; set; }

		[PositionByte(16)]
		public double RpmCurrent { get; set; }

		public double RpmCurrentPourcentage
		{
			get
			{
				if (this.RpmCurrent == 0 ||
				    this.RpmMax == 0)
				{
					return 0;
				}

				return this.RpmCurrent * 100 / this.RpmMax;
			}
		}

		public double RpmMax { get; set; }

		[PositionByte(276)]
		public double Fuel { get; set; }

		[PositionByte(307, 1)]
		public int Gear { get; set; }

		[PositionByte(256)]
		public double TempPneuFL { get; set; }

		[PositionByte(260)]
		public double TempPneuFR { get; set; }

		[PositionByte(264)]
		public double TempPneuRL { get; set; }

		[PositionByte(268)]
		public double TempPneuRR { get; set; }

		public CarInformationViewModel()
		{
			this.Vitesses = new List<double>();
			this.TempPneuFL = 32;
			this.TempPneuFR = 32;
			this.TempPneuRL = 32;
			this.TempPneuRR = 32;
			this.PropertyChanged += this.OnPropertyChanged;
		}
		
		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(this.RpmCurrent))
			{
				this.CalculRpmMax();
			}

			if (e.PropertyName == nameof(this.Vitesse))
			{
				this.CalculVistesseMoyenne();
			}
		}

		private void CalculVistesseMoyenne()
		{
			this.Vitesses.Add(this.Vitesse);
			this.OnPropertyChanged(nameof(this.VitesseMoyenne));
		}

		private void CalculRpmMax()
		{
			double max = Math.Max(this.RpmCurrent, this.RpmMax);
			this.RpmMax = Math.Ceiling(max / 1000) * 1000;
			this.OnPropertyChanged(nameof(this.RpmMax));
			this.OnPropertyChanged(nameof(this.RpmCurrentPourcentage));
		}

		public void EndRace()
		{
			this.Vitesse = 0;
			this.OnPropertyChanged(nameof(this.Vitesse));
			this.RpmCurrent = 0;
			this.OnPropertyChanged(nameof(this.RpmCurrent));
			this.Gear = -1;
			this.OnPropertyChanged(nameof(this.Gear));
			this.Acceleration = 0;
			this.OnPropertyChanged(nameof(this.Acceleration));
			this.Frein = 0;
			this.OnPropertyChanged(nameof(this.Frein));
		}
	}
}