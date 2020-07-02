using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProcessScheduling
{
    /// <summary>
    /// Interaction logic for DeleteProcess.xaml
    /// </summary>
    public partial class DeleteProcess : Window
    {
        public int SelectedId { get; private set; }
        public bool DeleteAll { get; private set; }
        public DeleteProcess(IEnumerable<int> ids, bool isWorkingAlgorithm)
        {
            InitializeComponent();
            Ids.ItemsSource = ids;
            SelectedId = -1;
            if (isWorkingAlgorithm)
                DeleteAllCheck.Visibility = Visibility.Hidden;
            DeleteAll = false;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteAll &&
                MessageBox.Show("Are you sure to delete all processes?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                Close();
            }
            else if (MessageBox.Show("Are you sure to delete "+Ids.SelectedItem.ToString()+" ?","",MessageBoxButton.YesNo,MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                SelectedId = int.Parse(Ids.SelectedItem.ToString());
                Close();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Ids.IsEnabled = false;
            DeleteAll = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Ids.IsEnabled = true;
            DeleteAll = false;
        }
    }
}
