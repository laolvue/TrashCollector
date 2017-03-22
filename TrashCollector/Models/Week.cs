using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Week
    {
        [Key]
        public int WeekId { get; set; }
        public string StartingWeek{ get; set; }
    }
}