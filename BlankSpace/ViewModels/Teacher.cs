using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        [Required]
        
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int UniversityId { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Phone")]

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Phone { get; set; }
        public int RoleTypeId { get; set; }
        public string RoleType { get; set; }
    }
}
