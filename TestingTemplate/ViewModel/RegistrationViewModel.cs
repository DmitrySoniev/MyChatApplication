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
        public ICommand RegistrationCommand { get; set; }

        public ICommand BackCommand { get; set; }

        public Action CloseAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

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

            if (RegistrationModel.Login == "" || RegistrationModel.Login == null)
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

            if (RegistrationModel.Name == "" || RegistrationModel.Name == null)
            {
                MessageBox.Show("Введите имя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RegistrationModel.Surname == "" || RegistrationModel.Surname == null)
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

            if (RegistrationModel.GenderMan == true)
            {
                MessageBox.Show("Регистрация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                //Должна быть логика
                MainWindow mainWindow = new MainWindow();
                CloseAction();
                mainWindow.ShowDialog();
            }

            if (RegistrationModel.GenderWoman == true)
            {
                MessageBox.Show("Регистрация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                //Должна быть логика
                MainWindow mainWindow = new MainWindow();
                CloseAction();
                mainWindow.ShowDialog();
            }
        }
    }
}