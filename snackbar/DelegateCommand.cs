using System;
using System.Windows.Input;

namespace snackbar
{
	public class DelegateCommand : ICommand
	{
		private readonly Action? execute;
		private readonly Action<object?>? argExecute;
		private readonly Func<bool>? canExecute;
		private readonly Func<object?, bool>? canArgExecute;

		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			this.execute = execute ?? throw new ArgumentNullException("execute");
			this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
		}

		public DelegateCommand(Action execute) : this(execute, () => true) { }

		public DelegateCommand(Action<object?> argExecute, Func<bool> canExecute)
		{
			this.argExecute = argExecute ?? throw new ArgumentNullException("execute");
			this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
		}

		public DelegateCommand(Action<object?>? argExecute, Func<object?, bool>? canArgExecute)
		{
			this.argExecute = argExecute ?? throw new ArgumentNullException("execute");
			this.canArgExecute = canArgExecute ?? throw new ArgumentNullException("canExecute");
		}

		bool ICommand.CanExecute(object? parameter)
		{
			if (this.canExecute != null) return this.canExecute();
			if (this.canArgExecute != null) return this.canArgExecute(parameter);

			throw new ArgumentNullException("canExecute");
		}

		public event EventHandler? CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public static void RaiseCanExecuteChanged()
		{
			CommandManager.InvalidateRequerySuggested();
		}

		void ICommand.Execute(object? parameter)
		{
			this.execute?.Invoke();
			this.argExecute?.Invoke(parameter);
		}
	}
}

