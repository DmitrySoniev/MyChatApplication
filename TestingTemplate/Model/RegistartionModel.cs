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

        public string login
        {
            get { return _login; }
            set
            {
                _login = value;
                this.OnPropertyChanged("LoginTextBox");
            }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.OnPropertyChanged("NameTextBox");
            }
        }

        private string _surname;
        public string surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                this.OnPropertyChanged("");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
