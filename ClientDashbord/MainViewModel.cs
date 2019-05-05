using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ClientDashbord.ViewModels;
using Newtonsoft.Json;

namespace ClientDashbord
{
	public class MainViewModel : BaseNotify
	{
		private CarDashViewModel _carDashViewModel;
		private bool _stopServerUpd;
		private readonly string _folderSaveRaceFile;

		public CarDashViewModel CarDashViewModel
		{
			get => this._carDashViewModel;
			set
			{
				this._carDashViewModel = value;
				this.OnPropertyChanged();
			}
		}

		public ICommand StartRaceCommand { get; }
		public ICommand EndRaceCommand { get; }
		public ICommand OpenRaceCommand { get; }

		public MainViewModel()
		{
			this.CarDashViewModel = new CarDashViewModel();
			this._folderSaveRaceFile = ConfigurationManager.AppSettings["FolderSaveRaceFile"];

			this.StartRaceCommand = new Command(this.ExecuteStartRace);
			this.EndRaceCommand = new Command(this.ExecuteEndRace);
			this.OpenRaceCommand = new Command(this.ExecuteOpenRace);
		}

		private void ExecuteStartRace()
		{
			this._stopServerUpd = false;
			this.Launch();
		}

		private void ExecuteEndRace()
		{
			this._stopServerUpd = true;

			this.CarDashViewModel.EndRace();
			string jsonRace = JsonConvert.SerializeObject(this.CarDashViewModel);
			string pathFile = Path.Combine(this._folderSaveRaceFile,
				$"Race_{DateTime.Now:yy-MM-dd}_{DateTime.Now:hhmmssffff}.race");
			File.WriteAllText(pathFile, jsonRace);
			this.CarDashViewModel = new CarDashViewModel();
		}

		private void Launch()
		{
			Task.Run(async () => this.LaunchClientUdp());
		}

		private void LaunchClientUdp()
		{
			int listenPort = Convert.ToInt32(ConfigurationManager.AppSettings["PortListen"]);
			UdpClient udpClient = new UdpClient(listenPort);
			IPEndPoint groupEp = new IPEndPoint(IPAddress.Any, listenPort);

			try
			{
				while (!this._stopServerUpd)
				{
					byte[] bytes = udpClient.Receive(ref groupEp);
					
					int isRaceOn = bytes.GetPartOfByteTab(new PositionByteAttribute(0)).ToInt();
					
					if (isRaceOn != 1)
					{
						continue;
					}

					Extension.ExecuteOnUi(() => this.CarDashViewModel.Update(bytes));
				}

				udpClient.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void ExecuteOpenRace()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = @"race file (*.race)|*.race";
			openFileDialog.InitialDirectory = this._folderSaveRaceFile;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Stream fileStream = openFileDialog.OpenFile();
				string jsonRace;

				using (StreamReader reader = new StreamReader(fileStream))
				{
					jsonRace = reader.ReadToEnd();
				}

				this.CarDashViewModel = JsonConvert.DeserializeObject<CarDashViewModel>(jsonRace);
			}
		}
	}
}