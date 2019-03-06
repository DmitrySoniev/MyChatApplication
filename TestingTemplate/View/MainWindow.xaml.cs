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

		private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				((MainViewModel)DataContext).AuthorizationCommand.Execute(PasswordBox);
			}
		}

		private void SendMessageTextbox_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				((MainViewModel)DataContext).SendMessageCommand.Execute(SendMessageTextBox);
			}
		}

		private void LoginTextBox_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				((MainViewModel)DataContext).AuthorizationCommand.Execute(null);
			}
		}

		private void FindFriendsTextbox_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				((MainViewModel)DataContext).FindUserCommand.Execute(FindUsersTextbox);
			}
		}
	}
}