using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [ForeignKey("Week")]
        public int WeekId { get; set; }
        public Week Week { get; set; }

        [ForeignKey("Time")]
        public int TimeId { get; set; }
        public Time Time { get; set; }

        [ForeignKey("Day")]
        public int DayId { get; set; }
        public Day Day { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}