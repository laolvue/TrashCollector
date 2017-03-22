using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class SetSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Preferred Pick up times:")]
        [ForeignKey("Time")]
        public int TimeId { get; set; }
        public Time Time { get; set; }

        [Required]
        [Display(Name = "Pick up days:")]
        [ForeignKey("Day")]
        public int DayId { get; set; }
        public Day Day { get; set; }
    }
}