using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using TestingTemplate.Model;


namespace TestingTemplate.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }

        public ICommand RegistrationCommand { get; set; }

        public ICommand FinishPassword { get; set; }

        public ICommand AuthorizationCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public MainViewModel()
        {
            LoginMainModel = new MainModel();
            FinishPassword = new RelayCommand(param => AuthCommand(param));

            AuthorizationCommand = new RelayCommand(param => AuthCommand(param));

            RegistrationCommand = new RelayCommand(param => Registration());

            AuthorizationCommand = new RelayCommand(param => AuthCommand(param));

            RegistrationCommand = new RelayCommand(param => Registration());
        }

        public MainModel LoginMainModel { get; set; }

        private void Registration()
        {
            Registration registrationWindow = new Registration();

            CloseAction();

            registrationWindow.ShowDialog();
        }

        private void AuthCommand(object param)
        {
            #region Zaglushka
            string login = "dmitry";
            string passwordCheck = "dmitry";
            #endregion Zaglushka
            #region CheckingForNullLoginAndPassword

            if (LoginMainModel.Login == "" || LoginMainModel.Login == null)
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

            if (LoginMainModel.Login != "" && password != "")
            {
                if (LoginMainModel.Login == login && password == passwordCheck)
                {
                    MessageBox.Show("Авторизация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Логин или пароль не совпадают, пожалуйста проверьте их и повторите попытку.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

        }
    }
    //public class users : INotifyPropertyChanged
    //{
    //    public ObservableCollection<MainModel> MainModels { get; set; }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}
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
}
