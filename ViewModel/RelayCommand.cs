using System;
using System.Windows.Input;

namespace Translator.ViewModel
{
	public class RelayCommand : ICommand
	{
		private Action _execute;
		private Func<object, bool> _canExecute;

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public RelayCommand(Action execute, Func<object, bool> canExecute = null)
		{
			this._execute = execute;
			this._canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute();
		}
	}
}
