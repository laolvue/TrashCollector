using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Month
    {
        [Key]
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}