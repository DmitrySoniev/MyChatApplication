using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using APIDog.Core;
using Microsoft.AspNet.SignalR.Client;
using Prism.Mvvm;
using Prism.Commands;

namespace TestingTemplate
{
    public class MainVM : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }
        public ICommand RegistrationCommand { get; set; }
        public ICommand AuthorizationCommand { get; set; }

        private string _login;

        public string loginTextBox
        {
            get { return _login; }
            set { _login = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public MainVM()
        {
            AuthorizationCommand = new RelayCommand(param => authCommand(param));
            RegistrationCommand = new RelayCommand(param => registration());
        }


        private void registration()
        {
            Registration registrationWindow = new Registration();
            CloseAction();
            registrationWindow.ShowDialog();
        }

        private void authCommand(object param)
        {
            Binding binding = new Binding();
            binding.ElementName ="LoginTextBox";
            
            var passwordBox = param as PasswordBox;

            if (passwordBox == null)
                return;

            var password = passwordBox.Password;

            if (password == "")
            {
                MessageBox.Show("Введите пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }

        //private void signinauth(object param)
        //{
        //    var login = param as TextBox;
        //    if (login == null)
        //        return;
        //    var loginTextBox = login.Text;
        //    if (loginTextBox == "")
        //    {
        //        MessageBox.Show("test");
        //    }
        //}
    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginTextBox.MaxLength = 15;
            PasswordBox.MaxLength = 15;
            SendMessageTextBox.Text = "Напишите сообщение...";
            MainVM mainViewModel = new MainVM();
            DataContext = mainViewModel;
            if (mainViewModel.CloseAction == null)
            {
                mainViewModel.CloseAction = new Action(this.Close);
            }
        }
        #region TESTMVVM



        //static class Test
        //{
        //    public static string GetHello(string a, string b) => a+b;
        //}

        //private string _login;
        //public string Login
        //{
        //    get { return _login; }
        //    set
        //    {
        //        _login = value;
        //        OnPropertyChanged("Login");
        //    }
        //}
        //public int Login { get; }=> 

        //public class MyModel : BindableBase
        //{
        //    private readonly ObservableCollection<string> _myvalues = new ObservableCollection<string>();

        //    public readonly ReadOnlyObservableCollection<string> MyPublicValues;

        //    public MyModel()
        //    {
        //        MyPublicValues = new ReadOnlyObservableCollection<string>(_myvalues);
        //    }
        //}
        //public class MainViewModel : INotifyPropertyChanged
        //{
        //    public event PropertyChangedEventHandler PropertyChanged;
        //    public void OnPropertyChanged([CallerMemberName] string prop = "")
        //    {
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        //    }
        //}
        //class DelegateCommand : ICommand
        //{
        //    private Action<Object> execute;
        //    private Func<object, bool> canExecute;
        //    public event EventHandler CanExecuteChanged
        //    {
        //        add { CommandManager.RequerySuggested += value; }
        //        remove { CommandManager.RequerySuggested -= value; }
        //    }

        //    public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        //    {
        //        this.execute = execute;
        //        this.canExecute = canExecute;
        //    }

        //public bool CanExecute(object parameter)
        //{
        //    return this.canExecute == null || this.canExecute(parameter);
        //}

        //public void Execute(object parameter)
        //{
        //    this.execute(parameter);
        //}
        //}

        //public ICommand Click
        //{
        //    get { return new DelegateCommand((obj) => { }); }
        //}

        //public class MyViewModel
        //{
        //    private ICommand _authorizationCommand;

        //    public ICommand AuthorizationCommand
        //    {
        //        get
        //        {
        //            return _authorizationCommand
        //                   ?? (_authorizationCommand = new ActionCommand(() => { MessageBox.Show("test"); }));
        //        }
        //    }
        //}

        #endregion TESTMVVM
        private void SendMessageTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SendMessageTextBox.Clear();
            SendMessageTextBox.Foreground = Brushes.Black;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration();
        }

        //private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Authorization();
        //}
        private void SendMessage()
        {
            if (SendMessageTextBox.Text == "")
            {
                SendMessageTextBox.Text = "Напишите сообщение...";
                SendMessageTextBox.Foreground = Brushes.DarkGray;
                return;
            }

            //Заглушка
            if (SendMessageTextBox.Text != "" && SendMessageTextBox.Text != "Напишите сообщение...")
            {
                SendMessageTextBox.Clear();
                MessageListBox.Items.Add("Привет!");
                return;
            }
        }

        private void Registration()
        {

        }

        public void Authorization()
        {
            string login = "dmitry";
            string password = "soniev";
            if (LoginTextBox.Text == "")
            {
                MessageBox.Show("Введите логин.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PasswordBox.Password == "")
            {
                MessageBox.Show("Введите пароль.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (LoginTextBox.Text != login && PasswordBox.Password != password)
            {
                MessageBox.Show(
                    "Пожалуйста, проверьте правильность написания логина и пароля. Повторите попытку позже.", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (LoginTextBox.Text == login && PasswordBox.Password == password)
            {
                MessageBox.Show("Авторизация прошла успешно!", "Успешно!", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
        }
    }
}