using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;

namespace ProcessScheduling
{
    public partial class MainWindow : Window
    {
        private Scheduling Scheduling;
        private List<Process> UnBlockedProcesses;
        private List<Process> BlockedProcesses;
        private List<IOResource> IOResources = new List<IOResource>
        {
            new IOResource{ Type=IOResourceType.Printer, Status=IOStatus.Ready },
            new IOResource{ Type=IOResourceType.Scanner, Status=IOStatus.Ready }
        };
        private DispatcherTimer ClockInterrupt;
        private int Interrupt;
        private int ClockCounter;
        private int lastId;
        private int Quantum;
        private bool isWorkingAlgorithm;
        //For Priority Scheduling
        private int PriorityForOther;
        private bool isRandomPriority;
        public MainWindow()
        {
            InitializeComponent();
            UnBlockedProcesses = new List<Process>();
            BlockedProcesses = new List<Process>();
            ClockInterrupt = new DispatcherTimer();
            ClockInterrupt.Interval = TimeSpan.FromMilliseconds(0.6);
            ClockInterrupt.Tick += ClockInterrupt_Tick;
            lastId = -1;
            Quantum = -1;
            Interrupt = -1;
            ClockCounter = 0;
            isWorkingAlgorithm = false;
            PriorityForOther = -20;
            isRandomPriority = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isWorkingAlgorithm)
                StartButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            Algorithms.IsEnabled = false;
            RandomProcess random = new RandomProcess();
            Process process = random.Generate();
            string selectedAlgorithm = Algorithms.SelectedItem.ToString();
            try
            {
                if (selectedAlgorithm.Contains("Round Robin"))
                {
                    process.Priority = 1;
                }
                else if (selectedAlgorithm.Contains("Static Priority"))
                {
                    if (lastId == -1)
                    {
                        AskRandomPriority ask = new AskRandomPriority();
                        ask.ShowDialog();
                        Interrupt = ask.Interrupt;
                        isRandomPriority = ask.isRandomPriority;
                    }
                    if (isRandomPriority)
                    {
                        process.Priority = new Random().Next(-19, 21);
                    }
                    else
                    {
                        if (PriorityForOther == -20)
                        {
                            AskPriority ask = new AskPriority();
                            ask.ShowDialog();
                            if (ask.isForOther.GetValueOrDefault())
                                PriorityForOther = ask.Priority;
                            process.Priority = ask.Priority;
                        }
                        else
                        {
                            process.Priority = PriorityForOther;
                        }
                    }
                }
                process.Id = ++lastId;
                process.Status = ProcessStatus.Ready;
                UnBlockedProcesses.Add(process);
                ReadyList.Items.Add(process);
                IDsForShow.Items.Add(process.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            isWorkingAlgorithm = true;
            Algorithms.IsEnabled = false;
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;

            AskQuantum ask = new AskQuantum();
            ask.ShowDialog();
            Quantum = ask.Quantum;
            Interrupt = ask.ClockInterrupt;
            if (Quantum == -1 || Interrupt == -1)
            {
                throw new Exception("Value of Quantum and Interrupt are not set");
            }
            string selectedAlgorithm = Algorithms.SelectedItem.ToString();
            if (selectedAlgorithm.Contains("Round Robin"))
            {
                Scheduling = new Scheduling(new RoundRobin(UnBlockedProcesses, Quantum));
            }
            else if (selectedAlgorithm.Contains("Static Priority"))
            {
                Scheduling = new Scheduling(new PriorityScheduling(UnBlockedProcesses, Quantum));
            }
            ClockInterrupt.Start();
        }
        private void ClockInterrupt_Tick(object sender, EventArgs e)
        {
            if (ClockCounter == Interrupt)
            {
                ClockCounter = 0;
                ProcessesRequiringIO();
                Scheduling.RefreshProcs(UnBlockedProcesses);
                Scheduling.CheckForContexSwitch();
                UnBlockedProcesses = Scheduling.List().ToList();
                if (Scheduling.isThereAnyThingToShow())
                {
                    RunningList.Items.Clear();
                    var runningProc = UnBlockedProcesses.FirstOrDefault(p => p.Status == ProcessStatus.Running);
                    if (runningProc != null)
                        RunningList.Items.Add(runningProc);

                    ReadyList.Items.Clear();
                    foreach (var proc in UnBlockedProcesses.Where(p => p.Status == ProcessStatus.Ready))
                        ReadyList.Items.Add(proc);

                    BlockedList.Items.Clear();
                    foreach (var proc in BlockedProcesses)
                        BlockedList.Items.Add(proc);
                }
            }
            else
            {
                Scheduling.IncrementTimer();
                ClockCounter++;
            }
        }
        private void ProcessesRequiringIO()
        {
            Process RunningProc = UnBlockedProcesses.FirstOrDefault(p => p.Status == ProcessStatus.Running);
            if (RunningProc != null && RunningProc.RequiredIOResource != IOResourceType.None)
            {
                foreach (var io in IOResources)
                {
                    if (RunningProc.RequiredIOResource == io.Type)
                    {
                        if (io.Status == IOStatus.Wait)
                        {
                            RunningProc.ToBlock();
                            BlockedProcesses.Add(RunningProc);
                            UnBlockedProcesses.Remove(RunningProc);
                        }
                    }
                }
            }
            var MustBeReady = from p in BlockedProcesses
                                from io in IOResources
                                where p.RequiredIOResource == io.Type
                                where io.Status == IOStatus.Ready
                                select p;
            foreach (var proc in MustBeReady.ToList())
            {
                proc.ToReady();
                BlockedProcesses.Remove(proc);
                UnBlockedProcesses.Add(proc);
            }
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            isWorkingAlgorithm = false;
            Algorithms.IsEnabled = true;
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            var runningProc = UnBlockedProcesses.FirstOrDefault(p => p.Status == ProcessStatus.Running);
            if (runningProc != null)
                runningProc.ToReady();
            ClockInterrupt.Stop();
        }
        private void Algorithms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddButton.IsEnabled = true;
        }
        private void Printer_Checked(object sender, RoutedEventArgs e)
        {
            var rbtn = sender as RadioButton;
            if (rbtn.Content.ToString().Contains("Ready"))
            {
                PrinterWait.IsChecked = false;
                IOResources[0].Status = IOStatus.Ready;
            }
            else
            {
                PrinterReady.IsChecked = false;
                IOResources[0].Status = IOStatus.Wait;
            }
        }

        private void Scanner_Checked(object sender, RoutedEventArgs e)
        {
            var rbtn = sender as RadioButton;
            if (rbtn.Content.ToString().Contains("Ready"))
            {
                ScannerWait.IsChecked = false;
                IOResources[1].Status = IOStatus.Ready;
            }
            else
            {
                ScannerReady.IsChecked = false;
                IOResources[1].Status = IOStatus.Wait;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var Processes = UnBlockedProcesses;
            var Ids = from p in Processes.Concat(BlockedProcesses)
                      select p.Id;
            DeleteProcess ask = new DeleteProcess(Ids,isWorkingAlgorithm);
            ask.ShowDialog();
            if (ask.DeleteAll)
            {
                DeleteButton.IsEnabled = false;
                StartButton.IsEnabled = false;
                UnBlockedProcesses.Clear();
                BlockedProcesses.Clear();
                ReadyList.Items.Clear();
                RunningList.Items.Clear();
                BlockedList.Items.Clear();
            }
            else if (ask.SelectedId != -1)
            {
                var SelectedProc = Processes.First(p => p.Id == ask.SelectedId);
                UnBlockedProcesses.Remove(SelectedProc);
                BlockedProcesses.Remove(SelectedProc);
            }
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            int targetID = int.Parse(IDsForShow.SelectedItem.ToString());
            var Procs = UnBlockedProcesses;
            Process targetProc = Procs.Concat(BlockedProcesses).First(p => p.Id == targetID);
            ShowProcess show = new ShowProcess(targetProc);
            show.ShowDialog();
        }

        private void IDsForShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowButton.IsEnabled = true;
        }
    }
}
