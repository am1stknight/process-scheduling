using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ProcessScheduling
{
    class RoundRobin : Algorithm
    {
        private Queue<Process> Processes;
        private int Quantum;
        private bool isThereAnyThingToShow;
        private Process Peek;
        public RoundRobin(List<Process> Processes, int Quantum)
        {
            this.Quantum = Quantum;
            isThereAnyThingToShow = false;
            this.Processes = new Queue<Process>();
            //all of processes has same priority
            foreach(var proc in Processes)
            {
                proc.Quantum = Quantum;
                proc.ToReady();
                this.Processes.Enqueue(proc);
            }
            Peek = this.Processes.Peek();
        }
        public void CheckForContexSwitch()
        {
            Peek = Processes.Peek();
            if (Peek.Status == ProcessStatus.Ready)
            {
                Peek.ToRunning();
                isThereAnyThingToShow = true;
            }
            else if (Peek.Status == ProcessStatus.Running)
            {
                if (Peek.Quantum == 0)
                {
                    Peek = Processes.Dequeue();
                    Peek.Quantum = Quantum;
                    Peek.ToReady();
                    Processes.Enqueue(Peek);
                    Peek = Processes.Peek();
                    Peek.ToRunning();
                    isThereAnyThingToShow = true;
                }
                else
                {
                    isThereAnyThingToShow = false;
                }
            }
        }
        public void IncrementTimer()
        {
            if (Peek.Quantum != 0)
                Peek.Quantum--;
        }
        bool Algorithm.isThereAnyThingToShow()
        {
            return isThereAnyThingToShow;
        }
        public IEnumerable<Process> List()
        {
            return Processes;
        }

        public void RefreshProcs(List<Process> processes)
        {
            Processes.Clear();
            foreach (var proc in processes)
                Processes.Enqueue(proc);
        }

    }
}
