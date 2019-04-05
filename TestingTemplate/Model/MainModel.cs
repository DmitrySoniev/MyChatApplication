using System.ComponentModel;

namespace TestingTemplate.Model
{
    public class MainModel : INotifyPropertyChanged
    {
        public string Login { get; set; }

        public string SendMessage { get; set; }

        public string PathToServer { get; set; }

        public string HelloUser { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}