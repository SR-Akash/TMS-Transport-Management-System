using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class PlaceVm
    {
        public int PlaceVmId { get; set; } 
        public int Serial { get; set; }
        [Required]
        [Display(Name = "Route Name")]
        public string PlaceName { get; set; }
    }
}
