using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestingTemplate.Model
{
	public class MainModel : INotifyPropertyChanged
	{
		private string _login;

		public string Login
		{
			get { return _login; }
			set
			{
				_login = value;
                OnPropertyChanged("LoginMainModel.Login");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
