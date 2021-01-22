using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Models
{
    public class AssignedDriver
    {
        [Key]
        public int AssignedDriverId { get; set; }
        public int BusId { get; set; }
        public Buses Bus { get; set; }
        
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

       




    }
}
