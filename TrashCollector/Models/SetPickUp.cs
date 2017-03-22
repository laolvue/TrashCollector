using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class SetPickUp
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Day> Day { get; set; }
        public ICollection<Time> Time { get; set; }

    }
}