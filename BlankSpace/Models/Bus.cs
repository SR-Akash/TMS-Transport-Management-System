using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Models
{
    public class Buses
    {
        [Key]
        public int BusId { get; set; }
        public string CoachName { get; set; }
        public int TotalSeat { get; set; }
        public int BusNumber { get; set; }
       

    }
}
