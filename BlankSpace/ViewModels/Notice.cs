using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class Notice
    {
        
        public int NoticeId { get; set; }
        [Required]
        [Display(Name = "Date")]
        public string NoticeDate { get; set; }
        [Required]
        [Display(Name = "Notice")]
        public string NoticeName { get; set; }
    }
}
