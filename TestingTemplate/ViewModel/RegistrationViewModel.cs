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

            var passwordBox = param as PasswordBox;
            if (passwordBox == null)
                return;

            var password = passwordBox.Password;

            if (RegistrationModel.GenderMan == false && RegistrationModel.GenderWoman == false)
            {
                MessageBox.Show("Пожалуйста укажите пол.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion CheckingValuesForNull

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
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Регистрация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);

                CloseAction();
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
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Регистрация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);

                CloseAction();
            }
        }
    }
}