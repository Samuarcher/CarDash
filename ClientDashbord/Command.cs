using System;
using System.Windows.Input;

namespace ClientDashbord
{
	public class Command<T> : ICommand
	{
		private Action<T> _execute;

		private event EventHandler CanExecuteChangedInternal;

		public Command(Action<T> execute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			this._execute = execute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
				this.CanExecuteChangedInternal += value;
			}

			remove
			{
				CommandManager.RequerySuggested -= value;
				this.CanExecuteChangedInternal -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			this._execute((T)parameter);
		}

		public void OnCanExecuteChanged()
		{
			EventHandler handler = this.CanExecuteChangedInternal;
			if (handler != null)
			{
				handler.Invoke(this, EventArgs.Empty);
			}
		}

		public void Destroy()
		{
			this._execute = null;
		}
	}

	public class Command : ICommand
	{
		private Action _execute;

		private event EventHandler CanExecuteChangedInternal;

		public Command(Action execute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			this._execute = execute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
				this.CanExecuteChangedInternal += value;
			}

			remove
			{
				CommandManager.RequerySuggested -= value;
				this.CanExecuteChangedInternal -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			this._execute();
		}

		public void OnCanExecuteChanged()
		{
			EventHandler handler = this.CanExecuteChangedInternal;
			if (handler != null)
			{
				handler.Invoke(this, EventArgs.Empty);
			}
		}

		public void Destroy()
		{
			this._execute = null;
		}
	}
}