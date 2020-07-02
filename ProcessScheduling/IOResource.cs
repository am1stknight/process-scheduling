using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScheduling
{
    public enum IOStatus
    {
        Wait, Ready
    }
    public enum IOResourceType
    {
        Printer, Scanner, None
    }
    public class IOResource
    {
        public IOResourceType Type { get; set; }
        public IOStatus Status { get; set; }
    }
}
