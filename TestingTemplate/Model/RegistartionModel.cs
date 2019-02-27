using System.ComponentModel;


namespace TestingTemplate.Model
{
    public class RegistrationModel : INotifyPropertyChanged
    {
        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        private bool _genderMan;
        public bool GenderMan
        {
            get { return _genderMan; }
            set
            {
                _genderMan = value;
                OnPropertyChanged("GenderMan");
            }
        }

        private bool _genderWoman;
        public bool GenderWoman
        {
            get { return _genderWoman; }
            set
            {
                _genderWoman = value;
                OnPropertyChanged("GenderWoman");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
