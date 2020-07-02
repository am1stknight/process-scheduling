using System.Windows;

namespace ProcessScheduling
{
    
    public partial class ShowProcess : Window
    {
        public ShowProcess(Process process)
        {
            InitializeComponent();
            Id.Content = process.Id;
            Priority.Content = process.Priority.ToString();
            IO.Content = process.IOshare;
            CPU.Content = process.CPUshare;
            Status.Content = process.Status;
            if (process.RequiredIOResource == IOResourceType.None)
            {
                RequiredResource.Visibility = Visibility.Hidden;
                RequiredLbl.Visibility = Visibility.Hidden;
            }
            else
                RequiredResource.Content = process.RequiredIOResource;
        }
    }
}
