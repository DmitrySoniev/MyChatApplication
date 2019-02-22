using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingTemplate.Model
{
	public class RegistrationModel : INotifyPropertyChanged
	{
		private string _login;

		public string Login
		{
			get { return _login; }
			set
			{
				_login = value;

			}
		}

		private string _name;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
			}
		}

		private string _surname;
		public string Surname
		{
			get { return _surname; }
			set
			{
				_surname = value;

			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
