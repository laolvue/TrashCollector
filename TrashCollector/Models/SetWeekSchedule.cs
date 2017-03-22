using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class SetWeekSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Week")]
        public int WeekId { get; set; }
        public Week Week { get; set; }

        [Required]
        [ForeignKey("Time")]
        public int TimeId { get; set; }
        public Time Time { get; set; }

        [Required]
        [ForeignKey("Day")]
        public int DayId { get; set; }
        public Day Day { get; set; }
    }
}