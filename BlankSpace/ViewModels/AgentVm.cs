using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class AgentVm
    {  
        public int AgentId { get; set; }
        public int Serial { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Date Of Birth")]
        public DateTime DOB { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Int64 Mobile { get; set; }
    }
}
