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
    }
}