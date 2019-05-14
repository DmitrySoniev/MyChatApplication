using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TestingTemplate.Model;

namespace TestingTemplate.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Client _client;

        public MainViewModel()
        {
            MainModel = new MainModel();

            AuthorizationCommand = new RelayCommand(AuthCommand);

            RegistrationCommand = new RelayCommand(param => Registration());

            SendMessageCommand = new RelayCommand(obj => SendCommand());

            //ClearMessagesCommand = new RelayCommand(param => ClearMessages());

            Messages = new ObservableCollection<string>();

            Users = new ObservableCollection<string>();

            LoadChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Action CloseAction { get; set; }

        public string CurrentUser { get; set; }

        public MainModel MainModel { get; set; }

        public string Message { get; set; }

        public ObservableCollection<string> Messages { get; set; }

        public ObservableCollection<string> Users { get; set; }

        private void AuthCommand(object param)
        {
            #region CheckingForValues

            if (string.IsNullOrEmpty(MainModel.Login))
            {
                MessageBox.Show("Введите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var passwordBox = param as PasswordBox;
            if (passwordBox == null)
                return;

            var password = passwordBox.Password;

            if (password == "")
            {
                MessageBox.Show("Введите пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(MainModel.PathToServer))
            {
                MessageBox.Show("Введите путь до сервера!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion CheckingForValues

            #region ConnectToServer

            string ServerAddress = "http://" + MainModel.PathToServer;

            _client = new Client(ServerAddress);

            _client.ReceiveMessage = (sender, message) =>
            {
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() =>

                   Messages.Add(sender + " " + message)));
            };
            SaveChanges();

            #endregion ConnectToServer

            if (!string.IsNullOrEmpty(MainModel.Login) && !string.IsNullOrEmpty(password))
            {
                if (MainModel.Login.Length > 40)
                {
                    MessageBox.Show("Логин должен не превышать 40 символов!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                if (password.Length > 20)
                {
                    MessageBox.Show("Пароль не должен превышать 20 символов!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                try
                {
                    _client.Login(MainModel.Login, password);

                    _client.StartChat();

                    var users = _client.GetAllUsers();
                    _client.GetAllUsers();
                    foreach (var item in users)
                    {
                        Users.Add(item);
                    }
                    MainModel.UserLogin = "Здравствуйте:" + "\n" + MainModel.Login;

                    MainModel.Login = String.Empty;

                    passwordBox.Password = String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        //private void ClearMessages()
        //{
        //    Messages.Clear();
        //}

        private void LoadChanges()
        {
            MainModel.PathToServer = Properties.Settings.Default.PathServer;
            Properties.Settings.Default.Save();
        }

        private void Registration()
        {
            if (string.IsNullOrEmpty(MainModel.PathToServer))
            {
                MessageBox.Show("Введите путь до сервера!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string ServerAddress = "http://" + MainModel.PathToServer;

            SaveChanges();
            ServerClass.ServerAdress = ServerAddress;

            Registration registrationWindow = new Registration();

            //CloseAction();

            registrationWindow.ShowDialog();
        }

        private void SaveChanges()
        {
            Properties.Settings.Default.PathServer = MainModel.PathToServer;
            Properties.Settings.Default.Save();
        }

        #region Commands

        public ICommand AuthorizationCommand { get; set; }
        //public ICommand ClearMessagesCommand { get; set; }

        public ICommand RegistrationCommand { get; set; }
        public ICommand SendMessageCommand { get; set; }

        #endregion Commands

        private void SendCommand()
        {
            if (string.IsNullOrEmpty(CurrentUser))
            {
                MessageBox.Show("Пользователь не выбран!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(Message))
            {
                MessageBox.Show("Введите сообщение!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (Message.Length > 500)
            {
                MessageBox.Show("Сообщение не должно превышать 500 символов!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _client.SendMessage(CurrentUser, Message);
            Message = String.Empty;
        }
    }
}