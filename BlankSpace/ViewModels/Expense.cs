using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.ViewModels
{
    public class Expense
    {

        public int ExpenseId { get; set; }
        public int BusId { get; set; }
        public string BusName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Amount { get; set; }
        [Required]
        public string Narration { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string DriverName { get; set; }
    }
}
