using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class BookingVm
    {
        public int BookingVmId { get; set; }
        [Display(Name = "Journey Start From")]
        [Remote(action: "CheckRoute", controller: "TicketBooking", ErrorMessage = "You Don't Have Bus From This Place")]
        public int StartingFrom { get; set; }
        [Required]
        public int Destination { get; set; }
        [Required]
        public int BusId { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public DateTime Date { get; set; } 
        public string CoachName { get; set; }
    }
}
