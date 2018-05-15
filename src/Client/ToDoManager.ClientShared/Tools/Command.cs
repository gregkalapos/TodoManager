using System;
using System.Windows.Input;

namespace ToDoManager.ClientShared.Tools
{
	public class Command : ICommand
	{
		private Action _action;

		public Command(Action action)
		{
			_action = action;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if(_action != null)
				_action();
		}
	}
}
