using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpApp
{
    class ServiceTask
    {
        public int TaskId { get; set; }
        public string TypeOfService { get; set; }
        public string Status { get; set; }
        public System.DateTime TimeIssued { get; set; }
        public Nullable<System.DateTime> TimeCompleted { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; }

        public virtual Room Room { get; set; }
    }
}
