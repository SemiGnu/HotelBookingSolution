using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpApp.Models
{
    public enum TypeOfTask { Cleaning, Maintenance, Service }
    public enum Status { New, InProgress, Finished }

    public class WorkerTask
    {
        public WorkerTask(TypeOfTask taskType, int roomId)
        {
            Type = taskType;
            TimeIssued = DateTime.Now;
            RoomId = roomId;
        }
        
        public TypeOfTask Type { get; set; }
        public DateTime TimeIssued { get; set; }

        public DateTime TimeCompleted { get; set; }

        public int RoomId { get; set; }
        public List<string> Notes { get; set; }

}

   
}
