using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class TicketReservationVm
    {
        public int TicketReservationId { get; set; }
        public bool ConfirmStatus { get; set; }
        public DateTime Date { get; set; }
        public int BusScheduleId { get; set; }
        public int AgentId { get; set; }
        public int PassengerId { get; set; }
        public string SeatNumber { get; set; } 
        public bool MightBeReserve{ get; set; }  

    }
       
}
