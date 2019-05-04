using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClientDashbord.Annotations;

namespace ClientDashbord
{
	public class BaseNotify : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}