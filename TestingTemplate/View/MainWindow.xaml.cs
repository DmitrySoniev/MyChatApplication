using System;
using System.Windows;
using System.Windows.Input;
using TestingTemplate.ViewModel;

namespace TestingTemplate
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
            if (mainViewModel.CloseAction == null)
            {
                mainViewModel.CloseAction = new Action(Close);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                ((MainViewModel)DataContext).AuthorizationCommand.Execute(PasswordBox);
            }
        }
    }
}