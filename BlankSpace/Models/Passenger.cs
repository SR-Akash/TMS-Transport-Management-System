using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Models
{
    public class Passenger
    {
        public Passenger()
        {
            TicketReservations = new HashSet<TicketReservation>();
        }
        [Key]
        public int PassengerId { get; set; }
        public string Name { get; set; }
        public Int64 Mobile { get; set; }


        public virtual ICollection<TicketReservation> TicketReservations { get; set; }






    }
}
