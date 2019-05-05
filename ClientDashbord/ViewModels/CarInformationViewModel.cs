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
		public double AccelerationPourcentage => this.Acceleration * 100 / 255;

		[PositionByte(304, 1)]
		public int Frein { get; set; }
		public double FreinPourcentage => this.Frein * 100 / 255;

		[PositionByte(306, 1)]
		public int FreinAMain { get; set; }


		[PositionByte(244)]
		public double Vitesse { get; set; }
		public double VitesseKmParH => this.Vitesse * 3.6;
		public List<double> Vitesses { get; set; }
		public double VitesseMoyenne => this.Vitesses.Sum() / this.Vitesses.Count;
		public double VitesseMoyenneKmParH => this.VitesseMoyenne * 3.6;


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
		public double FuelPourcentage => this.Fuel * 100;

		[PositionByte(307, 1)]
		public int Gear { get; set; }

		public CarInformationViewModel()
		{
			this.Vitesses = new List<double>();
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
			this.OnPropertyChanged(nameof(this.VitesseMoyenneKmParH));
		}

		private void CalculRpmMax()
		{
			double max = Math.Max(this.RpmCurrent, this.RpmMax);
			this.RpmMax = Math.Ceiling(max / 1000) * 1000;
			this.OnPropertyChanged(nameof(this.RpmMax));
			this.OnPropertyChanged(nameof(this.RpmCurrentPourcentage));
		}
	}
}