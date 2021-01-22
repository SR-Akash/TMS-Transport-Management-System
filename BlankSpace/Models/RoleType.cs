using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Models
{
    public class RoleType
    {
        [Key]
        public int RoleTypeId { get; set; }
        public string RoleName { get; set; }

    }
}
