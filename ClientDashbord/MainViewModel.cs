using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
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
		private bool _stopShowRamUse;
		private readonly string _folderSaveRaceFile;
		private long _ramUse;

		public ObservableCollection<CarDashViewModel> CarDashViewModels { get; set; }

		public CarDashViewModel CarDashViewModel
		{
			get => this._carDashViewModel;
			set
			{
				this._carDashViewModel = value;
				this.OnPropertyChanged();
			}
		}

		public long RamUse
		{
			get => this._ramUse;
			set
			{
				this._ramUse = value;
				this.OnPropertyChanged();
			}
		}

		public ICommand StartRaceCommand { get; }
		public ICommand EndRaceCommand { get; }
		public ICommand OpenRaceCommand { get; }
		public ICommand CloseRaceCommand { get; }

		public MainViewModel()
		{
			this.CarDashViewModels = new ObservableCollection<CarDashViewModel>();
			this._folderSaveRaceFile = ConfigurationManager.AppSettings["FolderSaveRaceFile"];

			this.StartRaceCommand = new Command(this.ExecuteStartRace);
			this.EndRaceCommand = new Command(this.ExecuteEndRace);
			this.OpenRaceCommand = new Command(this.ExecuteOpenRace);
			this.CloseRaceCommand = new Command<CarDashViewModel>(this.ExecuteCloseRace);

			Thread ramUsesThread = new Thread(this.SetRamUse);
			ramUsesThread.Start();
		}

		private void ExecuteCloseRace(CarDashViewModel carDashViewModel)
		{
			this.CarDashViewModels.Remove(carDashViewModel);
		}

		private void ExecuteStartRace()
		{
			this.CarDashViewModel = new CarDashViewModel();
			this.CarDashViewModels.Add(this.CarDashViewModel);
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
			Thread udpClientThread = new Thread(this.LaunchClientUdp);
			udpClientThread.Start();
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

		private void SetRamUse()
		{
			try
			{
				while (!this._stopShowRamUse)
				{
					Process proc = Process.GetCurrentProcess();
					this.RamUse = proc.PrivateMemorySize64;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void ExecuteOpenRace()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = @"race file (*.race)|*.race",
				InitialDirectory = this._folderSaveRaceFile
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Stream fileStream = openFileDialog.OpenFile();
				string jsonRace;

				using (StreamReader reader = new StreamReader(fileStream))
				{
					jsonRace = reader.ReadToEnd();
				}

				this.CarDashViewModel = JsonConvert.DeserializeObject<CarDashViewModel>(jsonRace);
				this.CarDashViewModels.Add(this.CarDashViewModel);
			}
		}

		public void CloseApp()
		{
			this._stopShowRamUse = true;
		}
	}
}