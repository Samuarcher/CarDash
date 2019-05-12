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

		public string NomCircuit { get; set; }

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

		public ObservableCollection<PisteViewModel> Pistes { get; }

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

		public CircuitViewModel(string nomCircuit)
		{
			this.NomCircuit = nomCircuit;

			this.Pistes = new ObservableCollection<PisteViewModel>();

			this.AjouterPisteCommand = new Command(this.ExecuteAjouterPiste);
		}

		private void ExecuteAjouterPiste()
		{
			this.Piste = new PisteViewModel(this.PisteName);
			this.Pistes.Add(this.Piste);
			this.PisteName = String.Empty;
		}
	}
}