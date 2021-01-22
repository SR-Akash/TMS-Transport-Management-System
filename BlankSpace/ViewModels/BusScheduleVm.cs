using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class BusScheduleVm
    {
        public int BusScheduleId { get; set; }
        [Display(Name = " From")]
        public int StartingFrom { get; set; }
        public int Destination { get; set; }
        public string StartingFromName { get; set; }
        public string DestinationName { get; set; }
        public string Time { get; set; }
        [Display(Name = "Driver No")]
        public int TicketPrice { get; set; }
        [Required]
        [Display(Name = "Bus Name ")]
        public int BusId { get; set; }
        public int Serial { get; set; }
        [Display(Name = "Bus Name ")]
        public string CoachName { get; set; } 


       
    }
}
