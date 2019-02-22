using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TestingTemplate.Model;

namespace TestingTemplate.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private RegistrationModel _registrationViewModel;

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
            _registrationViewModel = new RegistrationModel();
            RegistrationCommand = new RelayCommand(param => registration(param));
            BackCommand = new RelayCommand(param => back());
        }

        public RegistrationModel registrationModel
        {
            get { return _registrationViewModel; }
            set { _registrationViewModel = value; }
        }

        public void back()
        {
            MainWindow mainWindow = new MainWindow();
            CloseAction();
            mainWindow.ShowDialog();
        }

        public void registration(object param)
        {
            if (registrationModel.login == null)
            {
                MessageBox.Show("Введите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (registrationModel.login == "")
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

            if (registrationModel.name == null)
            {
                MessageBox.Show("Введите имя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (registrationModel.name == "")
            {
                MessageBox.Show("Введите имя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (registrationModel.surname == null)
            {
                MessageBox.Show("Введите фамилию!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (registrationModel.surname == "")
            {
                MessageBox.Show("Введите Фамилию!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           
        }
    }




}
