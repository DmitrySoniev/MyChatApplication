using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestingTemplate
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private HubConnection connection;

        public Registration()
        {
            InitializeComponent();
            LoginTextBox.MaxLength = 15;
            PasswordBox.MaxLength = 15;
            ConfirmPasswordBox.MaxLength = 15;
            NameTextbox.MaxLength = 15;
            SurnameTextbox.MaxLength = 15;
            //connection = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder()
            //    .WithUrl("https://localhost:44375/Chat")
            //    .Build();
            //connection.On<string, string>("ReceiveMessage", Print);
            //connection.StartAsync().Wait();
            
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {

            if (LoginTextBox.Text == "")
            {
                MessageBox.Show("Введите логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PasswordBox.Password == "")
            {
                MessageBox.Show("Введите пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ConfirmPasswordBox.Password == "")
            {
                MessageBox.Show("Введите подтвержденный пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NameTextbox.Text == "")
            {
                MessageBox.Show("Введите имя пользователя.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SurnameTextbox.Text == "")
            {
                MessageBox.Show("Введите фамилию пользователя.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (WomanRadioButton.IsChecked == false && ManRadioButton.IsChecked == false)
            {
                MessageBox.Show("Пожалуйста укажите пол.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (LoginTextBox.Text != "" && PasswordBox.Password != "" && ConfirmPasswordBox.Password != "" && NameTextbox.Text != "" && SurnameTextbox.Text != "")
            {
                if (PasswordBox.Password == ConfirmPasswordBox.Password)
                {
                    
                    if (ManRadioButton.IsChecked == true)
                    {
                        string loginMan;
                        string passwordMan;
                        string nameMan;
                        string surnameMan;
                        string genderMan;
                        loginMan = LoginTextBox.Text;
                        passwordMan = ConfirmPasswordBox.Password;
                        nameMan = NameTextbox.Text;
                        surnameMan = SurnameTextbox.Text;
                        genderMan = ManRadioButton.Content.ToString();
                        MessageBox.Show("Регистрация прошла успешно!", "Успешно!", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        MainWindow mainWindow = new MainWindow();
                        this.Close();
                        mainWindow.ShowDialog();
                    }


                    if (WomanRadioButton.IsChecked == true)
                    {
                        string loginWoman;
                        string passwordWoman;
                        string nameWoman;
                        string surnameWoman;
                        string genderWoman;
                        loginWoman = LoginTextBox.Text;
                        passwordWoman = ConfirmPasswordBox.Password;
                        nameWoman = NameTextbox.Text;
                        surnameWoman = SurnameTextbox.Text;
                        genderWoman = WomanRadioButton.Content.ToString();
                        MessageBox.Show("Регистрация прошла успешно!", "Успешно!", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        MainWindow mainWindow = new MainWindow();
                        this.Close();
                        mainWindow.ShowDialog();
                    }

                }
            }

        }
        public static void Print()
        {

        }

    }
}
