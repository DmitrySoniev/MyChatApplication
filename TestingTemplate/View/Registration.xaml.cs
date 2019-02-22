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
            //connection = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder()
            //    .WithUrl("https://localhost:44375/Chat")
            //    .Build();
            //connection.On<string, string>("ReceiveMessage", Print);
            //connection.StartAsync().Wait();           
        }
    }
}
