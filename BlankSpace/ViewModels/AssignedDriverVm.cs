using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class AssignedDriverVm
    {
        public int AssignedDriverId { get; set; }
        [Required]
        [Display(Name ="Bus Name")]
        public int BusId { get; set; }

        [Required]
        [Display(Name = "Driver Name")]
        public int DriverId { get; set; }

        public int Serial { get; set; }
        public int TotalSeat { get; set; }
        public string DriverName { get; set; }
        public Int64 DriverMobile { get; set; }

        public int BusNumber { get; set; }
        public string CoachName { get; set; }



    }
}
