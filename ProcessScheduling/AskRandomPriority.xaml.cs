using System.Windows;

namespace ProcessScheduling
{
    public partial class AskRandomPriority : Window
    {
        public bool isRandomPriority { get; private set; }
        public int Interrupt { get; private set; }
        public AskRandomPriority()
        {
            InitializeComponent();
            isRandomPriority = true;
        }
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            isRandomPriority = true;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            isRandomPriority = false;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Yes.IsChecked.Value)
                isRandomPriority = true;
            else if (No.IsChecked.Value)
                isRandomPriority = false;
            Interrupt = int.Parse(InterruptValue.Text);
            Close();
        }
    }
}
