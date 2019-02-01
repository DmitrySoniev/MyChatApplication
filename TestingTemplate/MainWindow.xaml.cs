using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.AspNet.SignalR.Client;

namespace TestingTemplate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginTextBox.MaxLength = 15;
            PasswordBox.MaxLength = 15;
            SendMessageTextBox.Text = "Напишите сообщение...";
      
        }

      
      
        private void SendMessageTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SendMessageTextBox.Clear();
            SendMessageTextBox.Foreground = Brushes.Black;
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (SendMessageTextBox.Text == "")
            {
                SendMessageTextBox.Text = "Напишите сообщение...";
                SendMessageTextBox.Foreground = Brushes.DarkGray;
                return;
            }
            //Заглушка
            if (SendMessageTextBox.Text != "")
            {
                SendMessageTextBox.Clear();
                MessageListBox.Items.Add("Привет!");
                return;
            }

        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();
            this.Close();
            registrationWindow.ShowDialog();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Пожалуйста, проверьте правильность написания логина и пароля. Повторите попытку позже.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LoginTextBox.Text == login && PasswordBox.Password == password)
            {
                MessageBox.Show("Авторизация прошла успешно!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
    }
}
