using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace snackbar
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual bool SetProperty<T>(
			ref T storage,
			T value,
			[CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

			storage = value;
			this.OnPropertyChanged(propertyName);

			return true;
		}

		protected virtual bool SetProperty<T>(
			ref T storage,
			T value,
			Action onChanged,
			[CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

			storage = value;
			onChanged?.Invoke();
			this.OnPropertyChanged(propertyName);

			return true;
		}

		protected virtual void OnPropertyChanged(
			[CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(
				this,
				new PropertyChangedEventArgs(propertyName));
		}
	}
}

