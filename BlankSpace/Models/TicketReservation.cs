using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Models
{
    public class TicketReservation
    {
        [Key]
        public int TicketReservationId { get; set; }
        public bool ConfirmStatus { get; set; }
        public string SeatNumber { get; set; } 
        public DateTime Date { get; set; }
        public int BusScheduleId { get; set; }
        public BusSchedule BusSchedule { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }



    }
}
