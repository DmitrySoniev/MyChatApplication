﻿using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Windows;
using TestingTemplate.ViewModel;

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
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            DataContext = registrationViewModel;
            if (registrationViewModel.CloseAction == null)
            {
                registrationViewModel.CloseAction = new Action(Close);
            }
        }
    }
}