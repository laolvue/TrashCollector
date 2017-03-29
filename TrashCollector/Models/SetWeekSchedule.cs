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
        [Display(Name = "Week")]
        [ForeignKey("Week")]
        public int WeekId { get; set; }
        public Week Week { get; set; }

        [Required]
        [Display(Name = "Time")]
        [ForeignKey("Time")]
        public int TimeId { get; set; }
        public Time Time { get; set; }

        [Required]
        [Display(Name = "Day")]
        [ForeignKey("Day")]
        public int DayId { get; set; }
        public Day Day { get; set; }
    }
}