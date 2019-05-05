using System.Windows;

namespace ClientDashbord
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			this.InitializeComponent();

			this.DataContext = new MainViewModel();
		}
	}
}
