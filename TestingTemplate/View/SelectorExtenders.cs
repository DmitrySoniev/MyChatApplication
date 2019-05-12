using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
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
                ((MainViewModel)DataContext).SendMessageCommand.Execute(null);
            }
        }

        private void LoginTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((MainViewModel)DataContext).AuthorizationCommand.Execute(null);
            }
        }

        private void MessageListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageListBox.Items.MoveCurrentToLast();
            MessageListBox.ScrollIntoView(MessageListBox.Items.CurrentItem);
        }
    }

    public class SelectorExtenders : DependencyObject
    {
        public static bool GetIsAutoscroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAutoscrollProperty);
        }

        public static void SetIsAutoscroll(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAutoscrollProperty, value);
        }

        public static readonly DependencyProperty IsAutoscrollProperty =
            DependencyProperty.RegisterAttached("IsAutoscroll", typeof(bool), typeof(SelectorExtenders),
                new UIPropertyMetadata(default(bool), OnIsAutoscrollChanged));

        public static void OnIsAutoscrollChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var val = (bool)e.NewValue;
            var lb = s as ListBox;
            var ic = lb.Items;
            var data = ic.SourceCollection as INotifyCollectionChanged;

            var autoscroller = new System.Collections.Specialized.NotifyCollectionChangedEventHandler(
                (s1, e1) =>
                {
                    object selectedItem = default(object);
                    switch (e1.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                        case NotifyCollectionChangedAction.Move:
                            selectedItem = e1.NewItems[e1.NewItems.Count - 1];
                            break;

                        case NotifyCollectionChangedAction.Remove:
                            if (ic.Count < e1.OldStartingIndex)
                            {
                                selectedItem = ic[e1.OldStartingIndex - 1];
                            }
                            else if (ic.Count > 0) selectedItem = ic[0];

                            break;

                        case NotifyCollectionChangedAction.Reset:
                            if (ic.Count > 0) selectedItem = ic[0];
                            break;
                    }

                    if (selectedItem != default(object))
                    {
                        ic.MoveCurrentTo(selectedItem);
                        lb.ScrollIntoView(selectedItem);
                    }
                });

            if (val) data.CollectionChanged += autoscroller;
            else data.CollectionChanged -= autoscroller;
        }
    }
}