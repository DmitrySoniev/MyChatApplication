using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestingTemplate.Model;

namespace TestingTemplate.ViewModel
{
    public class RegistrationViewModel
    {
        public ICommand RegistrationCommand { get; set; }

        public ICommand BackCommand { get; set; }

        public Action CloseAction { get; set; }

        public RegistrationViewModel()
        {
            RegistrationModel = new RegistrationModel();
            RegistrationCommand = new RelayCommand(param => Registration(param));
            BackCommand = new RelayCommand(param => Back());
        }

        public RegistrationModel RegistrationModel { get; set; }

        public void Back()
        {
            MainWindow mainWindow = new MainWindow();

            CloseAction();

            mainWindow.ShowDialog();
        }

        public void Registration(object param)
        {
            #region CheckingForNull

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

            #endregion CheckingForNull

            if (RegistrationModel.GenderMan)
            {
                var client = new Client(MainViewModel.ServerAddress);
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
                CloseAction();
                mainWindow.ShowDialog();
            }

            if (RegistrationModel.GenderWoman)
            {
                var client = new Client(MainViewModel.ServerAddress);
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