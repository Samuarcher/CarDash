using System;
using System.Windows;

namespace ClientDashbord
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private MainViewModel _mainViewModel;

		public MainWindow()
		{
			this.InitializeComponent();

			this._mainViewModel = new MainViewModel();
			this.DataContext = this._mainViewModel;
			this.Closed += OnClosed;
		}

		private void OnClosed(object sender, EventArgs e)
		{
			this._mainViewModel.CloseApp();
		}
	}
}
