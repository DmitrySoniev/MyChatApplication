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
        public const string ServerAddress = "http://192.168.1.67:50338";
        private Client _client;

        public MainViewModel()
        {
            LoginMainModel = new MainModel();

            AuthorizationCommand = new RelayCommand(AuthCommand);

            RegistrationCommand = new RelayCommand(param => Registration());

            SendMessageCommand = new RelayCommand(obj => SendCommand());

            ClearMessagesCommand = new RelayCommand(param => ClearMessages());

           

                Messages = new ObservableCollection<string>();

            Users = new ObservableCollection<string>();

            _client = new Client(ServerAddress);
            _client.ReceiveMessage = (sender, message) =>
            {
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() =>

                   Messages.Add(sender + " " + message)));
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Action CloseAction { get; set; }

        #region Commands

        public ICommand AuthorizationCommand { get; set; }
        public ICommand ClearMessagesCommand { get; set; }
        public ICommand FindUserCommand { get; set; }
        public ICommand RegistrationCommand { get; set; }
        public ICommand SendMessageCommand { get; set; }

        #endregion Commands

        public string CurrentUser { get; set; }
        public MainModel LoginMainModel { get; set; }
        public string Message { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public ObservableCollection<string> Users { get; set; }

        private void AuthCommand(object param)
        {
            #region CheckingForNullLoginAndPassword

            if (string.IsNullOrEmpty(LoginMainModel.Login))
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

            #endregion CheckingForNullLoginAndPassword

            if (!string.IsNullOrEmpty(LoginMainModel.Login) && !string.IsNullOrEmpty(password))
            {
                if (LoginMainModel.Login.Length > 15)
                {
                    MessageBox.Show("Логин должен не превышать 15 символов!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                if (password.Length > 20)
                {
                    MessageBox.Show("Пароль не должен превышать 20 символов!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                try
                {
                    _client.Login(LoginMainModel.Login, password);

                    _client.StartChat();

                    LoginMainModel.Login = String.Empty;

                    passwordBox.Password = String.Empty;
                    var users = _client.GetAllUsers();
                    _client.GetAllUsers();
                    foreach (var item in users)
                    {
                        Users.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ClearMessages()
        {
            Messages.Clear();
        }

        private void Registration()
        {
            Registration registrationWindow = new Registration();

            CloseAction();

            registrationWindow.ShowDialog();
        }

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
                MessageBox.Show("Сообщение не должно превышать 500 символов", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _client.SendMessage(CurrentUser, Message);
            Message = String.Empty;
        }
    }
}