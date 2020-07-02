using System;
using System.Windows;

namespace ProcessScheduling
{
    public partial class AskPriority : Window
    {
        public AskPriority()
        {
            InitializeComponent();
        }
        public int Priority { get; private set; }
        public bool? isForOther { get; private set; }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            int value=1;
            try
            {
                value = int.Parse(Value.Text);
                Priority = value;
                isForOther = checkForOther.IsChecked;
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter valid number", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
