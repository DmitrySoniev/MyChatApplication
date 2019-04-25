using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestingTemplate.Model;

namespace TestingTemplate.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public RegistrationViewModel()
        {
            RegistrationModel = new RegistrationModel();

            RegistrationCommand = new RelayCommand(param => Registration(param));
            BackCommand = new RelayCommand(param => Back());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackCommand { get; set; }
        public Action CloseAction { get; set; }
        public ICommand RegistrationCommand { get; set; }
        public RegistrationModel RegistrationModel { get; set; }

        public void Back()
        {
            if (MessageBox.Show("Вы действительно хотите вернуться?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
            }
            else
            {
                CloseAction();
            }
        }

        public void Registration(object param)
        {
            #region CheckingValuesForNull

            if (string.IsNullOrEmpty(RegistrationModel.Login))
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

            if (string.IsNullOrEmpty(RegistrationModel.Name))
            {
                MessageBox.Show("Введите имя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(RegistrationModel.Surname))
            {
                MessageBox.Show("Введите Фамилию!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RegistrationModel.GenderMan == false && RegistrationModel.GenderWoman == false)
            {
                MessageBox.Show("Пожалуйста укажите пол.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion CheckingValuesForNull

            #region ValuesLength

            if (RegistrationModel.Login.Length > 15)
            {
                MessageBox.Show("Логин не должен превышать 15 символов!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (password.Length > 20)
            {
                MessageBox.Show("Пароль не должен превышать 20 символов!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (RegistrationModel.Name.Length > 15)
            {
                MessageBox.Show("Имя не должно превышать 15 символов!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (RegistrationModel.Surname.Length > 25)
            {
                MessageBox.Show("Фамилия не должна превышать 25 символов!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            #endregion ValuesLength

            if (RegistrationModel.GenderMan)
            {
                var serverAddress = ServerClass.ServerAdress;
                var client = new Client(serverAddress);
                try
                {
                    client.Register(RegistrationModel.Login, password, RegistrationModel.Name, RegistrationModel.Surname, Client.Gender.Men);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                MessageBox.Show("Регистрация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainWindow = new MainWindow();

                mainWindow.ShowDialog();
            }

            if (RegistrationModel.GenderWoman)
            {
                var serverAddress = ServerClass.ServerAdress;
                var client = new Client(serverAddress);
                try
                {
                    client.Register(RegistrationModel.Login, password, RegistrationModel.Name, RegistrationModel.Surname, Client.Gender.Women);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                MessageBox.Show("Регистрация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                CloseAction();
                mainWindow.ShowDialog();
            }
        }
    }
}