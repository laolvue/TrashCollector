using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class EmployeeRouter
    {
        [Key]
        public int RouteId { get; set; }
        [ForeignKey("Zip")]
        public int ZipId { get; set; }
        public Zip Zip { get; set; }
        [ForeignKey("Week")]
        public int WeekId { get; set; }
        public Week Week { get; set; }
        [ForeignKey("Day")]
        public int DayId { get; set; }
        public Day Day { get; set; }
    }
}