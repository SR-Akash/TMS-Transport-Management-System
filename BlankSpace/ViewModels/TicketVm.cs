using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class TicketVm
    {
        public string PassengerName { get; set; }
        public Int64 Mobile { get; set; } 
        public List<string> Seat { get; set; } 
        public int TotalCost { get; set; }
        public DateTime Date { get; set; }
        public int TicketPrice { get; set; } 
        public string StartFrom { get; set; }
        public string Destination { get; set; } 
        public string BusName { get; set; }  
        public string Time { get; set; }   
    }
}
