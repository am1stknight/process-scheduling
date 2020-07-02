using System;
using System.Windows.Threading;

namespace ProcessScheduling
{
    public enum ProcessStatus
    {
        Running, Blocked, Ready, Complete
    }
    
    public class Process
    {
        public int Id { get; set; }
        public int Quantum { get; set; }
        public int Priority { get; set; }
        public ProcessStatus Status { get; set; }
        public int BlockedTime { get; set; }
        public int RunningTime { get; set; }/// <summary>
        /// 
        /// </summary>
        public int ReadyTime { get; set; }
        public IOResourceType RequiredIOResource { get; set; }
        public int IOshare { get; set; }
        public int CPUshare { get; set; }
        public void ToRunning()
        {
            Status = ProcessStatus.Running;
        }
        public void ToReady()
        {
            Status = ProcessStatus.Ready;
        }
        public void ToBlock()
        {
            Status = ProcessStatus.Blocked;
        }
        public void ToComplete()
        {
            Status = ProcessStatus.Complete;
        }
    }
}
