using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Models
{
    public class BusSchedule
    {
        public BusSchedule()
        {
            TicketReservations = new HashSet<TicketReservation>();
        }
        [Key]
        public int BusScheduleId { get; set; }
        public int TicketPrice { get; set; }
        public int StartingFrom { get; set; }
      
        [ForeignKey("StartingFrom")]
        public Place Place{ get; set; } 
        public int Destination { get; set; }
      
        public string Time { get; set; }
        public int BusId { get; set; }
        public Buses Bus { get; set; }
        public virtual ICollection<TicketReservation> TicketReservations { get; set; }
       
    }
}
