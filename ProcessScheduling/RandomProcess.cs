using System;

namespace ProcessScheduling
{
    class RandomProcess
    {
        public Process RandProcess { get; private set; }
        public RandomProcess()
        {
            RandProcess = new Process();
        }
        public Process Generate()
        {
            Random r = new Random();
            switch(r.Next(1,4))
            {
                case 1:
                    RandProcess.CPUshare = r.Next(1, 99);
                    RandProcess.IOshare = 100 - RandProcess.CPUshare;
                    RandProcess.RequiredIOResource = IOResourceType.Printer;
                    break;
                case 2:
                    RandProcess.CPUshare = r.Next(1, 99);
                    RandProcess.IOshare = 100 - RandProcess.CPUshare;
                    RandProcess.RequiredIOResource = IOResourceType.Scanner;
                    break;
                case 3:
                    RandProcess.CPUshare = 100;
                    RandProcess.IOshare = 0;
                    RandProcess.RequiredIOResource = IOResourceType.None;
                    break;
            }
            return RandProcess;
        }
    }

}
