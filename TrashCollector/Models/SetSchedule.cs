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
        [ForeignKey("Time")]
        public int PersonId { get; set; }
        public Time Time { get; set; }
        [ForeignKey("Day")]
        public int DayId { get; set; }
        public Day Day { get; set; }
    }
}