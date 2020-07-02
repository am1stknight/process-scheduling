using System.Collections.Generic;

namespace ProcessScheduling
{
    //Design pattern Stategy
    interface Algorithm
    {
        bool isThereAnyThingToShow();
        void CheckForContexSwitch();
        void IncrementTimer();
        IEnumerable<Process> List();
        void RefreshProcs(List<Process> processes);
    }
    //Contex class to using Strategies
    class Scheduling
    {
        private Algorithm algorithm;
        public Scheduling(Algorithm algorithm) =>
            this.algorithm = algorithm;
        public bool isThereAnyThingToShow() =>
            algorithm.isThereAnyThingToShow();
        public void CheckForContexSwitch() =>
            algorithm.CheckForContexSwitch();
        public void IncrementTimer() =>
            algorithm.IncrementTimer();
        public IEnumerable<Process> List() => 
            algorithm.List();
        public void RefreshProcs(List<Process> processes) =>
            algorithm.RefreshProcs(processes);
    }
}
