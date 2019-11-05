using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Models
{
    public class Journal
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 4, ErrorMessage ="Enter the full book name.")]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Enter a book name using only letters, numbers and spaces.")]
        [Required]
        public string Book { get; set; }
        [Range(1, 100)]
        public int Chapter { get; set; }
        [Range(1, 200)]
        public int Verse { get; set; }
        public string Note { get; set; }
        [Display(Name = "Date Entered")]
        [DataType(DataType.Date)]
        public DateTime RecordDate { get; set; }
    }
}
