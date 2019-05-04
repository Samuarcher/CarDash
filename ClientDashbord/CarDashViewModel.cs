namespace ClientDashbord
{
	public class CarDashViewModel : BaseNotify
	{
		private double _vitesse;
		public double Vitesse
		{
			get => this._vitesse;
			set
			{
				this._vitesse = value * 3.6;
				this.OnPropertyChanged();
			}
		}

		private int _acceleration;
		public int Acceleration
		{
			get => this._acceleration;
			set
			{
				this._acceleration = value;
				this.OnPropertyChanged();
			}
		}

		private double _rmpIdle;
		public double RmpIdle
		{
			get => this._rmpIdle;
			set
			{
				this._rmpIdle = value;
				this.OnPropertyChanged();
			}
		}

		private double _rpmCurrent;
		public double RpmCurrent
		{
			get => this._rpmCurrent;
			set
			{
				this._rpmCurrent = value;
				this.OnPropertyChanged();
			}
		}

		private double _fuel;
		public double Fuel
		{
			get => this._fuel;
			set
			{
				this._fuel = value * 100;
				this.OnPropertyChanged();
			}
		}
	}
}