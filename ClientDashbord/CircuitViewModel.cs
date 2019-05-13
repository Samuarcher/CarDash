using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ClientDashbord
{
	public class CircuitViewModel : BaseNotify
	{
		private PisteViewModel _piste;
		private string _pisteName;
		private double _distance;

		public string NomCircuit { get; set; }

		public string Pays { get; set; }

		public ObservableCollection<PisteViewModel> Pistes { get; }

		[JsonIgnore]
		public PisteViewModel Piste
		{
			get => this._piste;
			set
			{
				this._piste = value;
				this.OnPropertyChanged();
			}
		}

		[JsonIgnore]
		public ICommand AjouterPisteCommand { get; }

		[JsonIgnore]
		public string PisteName
		{
			get => this._pisteName;
			set
			{
				this._pisteName = value;
				this.OnPropertyChanged();
			}
		}

		[JsonIgnore]
		public double Distance
		{
			get => this._distance;
			set
			{
				this._distance = value;
				this.OnPropertyChanged();
			}
		}

		public CircuitViewModel(string nomCircuit,string pays)
		{
			this.NomCircuit = nomCircuit;
			this.Pays = pays;

			this.Pistes = new ObservableCollection<PisteViewModel>();

			this.AjouterPisteCommand = new Command(this.ExecuteAjouterPiste);
		}

		private void ExecuteAjouterPiste()
		{
			this.Piste = new PisteViewModel(this.PisteName, this.Distance);
			this.Pistes.Add(this.Piste);
			this.PisteName = String.Empty;
			this.Distance = 0;
		}
	}
}