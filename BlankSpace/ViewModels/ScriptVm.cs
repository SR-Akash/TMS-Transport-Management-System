using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class ScriptVm
    {
        public int ScriptVmId { get; set; }
        [Required]
        [Display(Name="Coach Name")]
        public int BusId { get; set; }
        [Required]
        public DateTime Date { get; set; } 
        public string Name { get; set; }  
        public string Route { get; set; }   
        public List<string> Seat { get; set; }    
        public Int64 Mobile  { get; set; }  
        public string BusName  { get; set; }   

    }
}
