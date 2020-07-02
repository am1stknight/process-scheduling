using System;
using System.Windows;

namespace ProcessScheduling
{
    /// <summary>
    /// Interaction logic for AskQuantum.xaml
    /// </summary>
    public partial class AskQuantum : Window
    {
        public AskQuantum()
        {
            InitializeComponent();
        }
        public int Quantum { get; private set; }
        public int ClockInterrupt { get; private set; }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantum = int.Parse(QuantumValue.Text);
                int clock = int.Parse(ClockValue.Text);
                if (quantum <= 0 || clock <= 0)
                    MessageBox.Show("Values must be positive","",MessageBoxButton.OK,MessageBoxImage.Error);
                else
                {
                    Quantum = quantum;
                    ClockInterrupt = clock;
                }
            }
            catch
            {
                MessageBox.Show("Error", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Close();
            }
        }
    }
}
