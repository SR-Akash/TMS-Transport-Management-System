using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Models
{
    public class Agent
    {   
        [Key]
        public int AgentId { get; set; }
        public Int64 Mobile { get; set; } 
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public virtual ICollection<TicketReservation> TicketReservations { get; set; }

    }
}
