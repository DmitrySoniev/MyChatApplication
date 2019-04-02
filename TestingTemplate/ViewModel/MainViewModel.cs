using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestingTemplate.Model;

namespace TestingTemplate.ViewModel
{
	public class MainViewModel
	{
		public Action CloseAction { get; set; }

		public ICommand RegistrationCommand { get; set; }

		public ICommand AuthorizationCommand { get; set; }

		public ICommand SendMessageCommand { get; set; }

		public ICommand FindUserCommand { get; set; }

		public MainViewModel()
		{
			LoginMainModel = new MainModel();

			AuthorizationCommand = new RelayCommand(AuthCommand);

			RegistrationCommand = new RelayCommand(param => Registration());

			SendMessageCommand = new RelayCommand(param => SendCommand());

			FindUserCommand = new RelayCommand(param => FindUser());
		}

		public MainModel LoginMainModel { get; set; }

		private void Registration()
		{
			Registration registrationWindow = new Registration();

			CloseAction();

			registrationWindow.ShowDialog();
		}

		private void FindUser()
		{
			if (string.IsNullOrEmpty(LoginMainModel.FindUser))
			{
				MessageBox.Show("FindUserTextboxTest");
			}
		}

		private void SendCommand()
		{
			if (string.IsNullOrEmpty(LoginMainModel.SendMessage))
			{
				MessageBox.Show("SendMessageTextBoxTest");
				return;
			}
		}

		private void AuthCommand(object param)
		{
			#region Заглушка

			string login = "dmitry";
			string passwordCheck = "dmitry";

			#endregion Заглушка

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