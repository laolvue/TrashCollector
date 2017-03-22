using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Day
    {
        [Key]
        public int DayId { get; set; }
        [Display(Name = "Day")]
        public string DayName { get; set; }
    }
}