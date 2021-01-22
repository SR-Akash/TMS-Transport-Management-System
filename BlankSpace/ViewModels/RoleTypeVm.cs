using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class RoleTypeVm
    {
        public int RoleTypeId { get; set; }
        [Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
        public int Serial { get; set; } 
    }
}
