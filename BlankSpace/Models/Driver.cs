using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        public string Name { get; set; }
        public Int64 Mobile { get; set; }
        public string Address  { get; set; }
        public string LicenseNumber { get; set; }

    }
}
