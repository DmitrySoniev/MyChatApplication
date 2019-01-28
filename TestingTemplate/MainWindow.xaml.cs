﻿using System;
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
            SendMessageTextBox.Text = "Напишите сообщение...";
        }

        private void SendMessageTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SendMessageTextBox.Clear();
            SendMessageTextBox.Foreground = Brushes.Black;
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessageTextBox.Text = "Напишите сообщение...";
            SendMessageTextBox.Foreground = Brushes.DarkGray;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();
            this.Close();
            registrationWindow.ShowDialog();
        }
    }
}
