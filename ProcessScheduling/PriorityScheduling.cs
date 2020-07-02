using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessScheduling
{
    class PriorityScheduling : Algorithm
    {
        private int Quantum;
        private List<Process> Processes;
        private Process highestProcess;
        private bool isThereAnyThingToShow;
        public PriorityScheduling(List<Process> Processes, int Quantum)
        {
            isThereAnyThingToShow = false;
            this.Processes = new List<Process>();
            foreach (var proc in Processes)
            {
                proc.Quantum = Quantum;
                proc.ToReady();
                this.Processes.Add(proc);
            }
            this.Quantum = Quantum;
            int highestPriority = (from p in Processes
                                   select p.Priority).Max();
            highestProcess = Processes.First(p => p.Priority == highestPriority);
        }
        public void CheckForContexSwitch()
        {
            if (highestProcess.Status == ProcessStatus.Ready)
            {
                highestProcess.ToRunning();
            }
            else if (highestProcess.Status == ProcessStatus.Running)
            {
                if (highestProcess.Quantum == 0)
                {
                    highestProcess.ToReady();
                    var nextProcs = from p in Processes
                                    where p != highestProcess && p.Priority <= highestProcess.Priority
                                    where p.Quantum != 0
                                    select p.Priority;
                    if (nextProcs.Count() != 0)
                    {
                        Process nextProcess = Processes.First(p => p != highestProcess && p.Quantum != 0 && p.Priority == nextProcs.Max());
                        nextProcess.ToRunning();
                        highestProcess = nextProcess;
                    }
                    else
                    {
                        foreach (var p in Processes)
                        {
                            p.Quantum = Quantum;
                        }
                        int highestPriority = (from p in Processes
                                               select p.Priority).Max();
                        highestProcess = Processes.First(p => p.Priority == highestPriority);
                    }
                }
            }
            else
            {
                // if highestProcess is block
                var validProcs = from p in Processes
                                 where p.Quantum != 0
                                 select p.Priority;
                int highestPriority;
                if (validProcs.Count() != 0)
                {
                    highestPriority = validProcs.Max();
                    highestProcess = Processes.First(p => p.Quantum != 0 && p.Priority == highestPriority);
                }
                else
                {
                    highestPriority = (from p in Processes
                                       select p.Priority).Max();
                    highestProcess = Processes.First(p => p.Priority == highestPriority);
                }
            }
            isThereAnyThingToShow = true;
        }
        public void IncrementTimer()
        {
            if (highestProcess.Quantum != 0)
                highestProcess.Quantum--;
        }

        public IEnumerable<Process> List()
        {
            return Processes;
        }

        public void RefreshProcs(List<Process> processes)
        {
            Processes = processes;
        }

        bool Algorithm.isThereAnyThingToShow()
        {
            return isThereAnyThingToShow;
        }

    }
}
