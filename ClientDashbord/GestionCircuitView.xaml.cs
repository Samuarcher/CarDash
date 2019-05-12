using System;
using System.Windows;

namespace ClientDashbord
{
	/// <summary>
	/// Logique d'interaction pour GestionCircuitView.xaml
	/// </summary>
	public partial class GestionCircuitView
	{
		public GestionCircuitView()
		{
			this.InitializeComponent();

			this.DataContext = new GestionCircuitViewModel();
		}
	}
}
