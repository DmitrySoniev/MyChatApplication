using System.ComponentModel;

namespace TestingTemplate.Model
{
	public class RegistrationModel : INotifyPropertyChanged
	{
		public string Login { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public bool GenderMan { get; set; }

		public bool GenderWoman { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}