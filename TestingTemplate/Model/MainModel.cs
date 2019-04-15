using System.ComponentModel;

namespace TestingTemplate.Model
{
    public class MainModel : INotifyPropertyChanged
    {
        public string Login { get; set; }

        public string SendMessage { get; set; }

        public string PathToServer { get; set; }

        /// <summary>
        /// Отображение логина пользователя
        /// </summary>
        public string UserLogin { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}