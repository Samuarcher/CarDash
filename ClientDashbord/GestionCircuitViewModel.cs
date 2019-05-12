using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ClientDashbord
{
	public class GestionCircuitViewModel : BaseNotify
	{
		private string _folderSaveRaceFile;
		private string _circuitNameFile;
		private CircuitViewModel _circuitViewModel;
		private string _circuitName;
		public ObservableCollection<CircuitViewModel> CircuitViewModels { get; set; }

		public CircuitViewModel CircuitViewModel
		{
			get => this._circuitViewModel;
			set
			{
				this._circuitViewModel = value;
				this.OnPropertyChanged();
			}
		}

		public string CircuitName
		{
			get => this._circuitName;
			set
			{
				this._circuitName = value;
				this.OnPropertyChanged();
			}
		}

		public ICommand AjouterCircuitCommand { get; }
		public ICommand SaveCommand { get; }

		public GestionCircuitViewModel()
		{
			this._folderSaveRaceFile = ConfigurationManager.AppSettings["FolderSaveRaceFile"];
			this._circuitNameFile = ConfigurationManager.AppSettings["CircuitFileName"];

			this.AjouterCircuitCommand = new Command(this.ExecuteAjouterCircuit);
			this.SaveCommand = new Command(this.ExecuteSave);

			string circuitFilePath = Path.Combine(this._folderSaveRaceFile, this._circuitNameFile);
			this.LoadCircuitFile(circuitFilePath);
		}

		private void LoadCircuitFile(string circuitFilePath)
		{
			if (!File.Exists(circuitFilePath))
			{
				FileStream fileStream = File.Create(circuitFilePath);
				fileStream.Close();
			}

			string jsonCircuit = File.ReadAllText(circuitFilePath);

			if (string.IsNullOrEmpty(jsonCircuit))
			{
				this.CircuitViewModels = new ObservableCollection<CircuitViewModel>();
				return;
			}

			this.CircuitViewModels = JsonConvert.DeserializeObject<ObservableCollection<CircuitViewModel>>(jsonCircuit);
			this.OnPropertyChanged(nameof(this.CircuitViewModels));
		}

		private void ExecuteSave()
		{
			string jsonRace = JsonConvert.SerializeObject(this.CircuitViewModels);
			string circuitFilePath = Path.Combine(this._folderSaveRaceFile, this._circuitNameFile);
			File.WriteAllText(circuitFilePath, jsonRace);
		}

		private void ExecuteAjouterCircuit()
		{
			this.CircuitViewModel = new CircuitViewModel(this.CircuitName);
			this.CircuitViewModels.Add(this.CircuitViewModel);
			this.CircuitName = String.Empty;
		}
	}
}