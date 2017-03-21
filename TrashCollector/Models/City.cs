using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        [ForeignKey("Zip")]
        public int ZipId { get; set; }
        public Zip Zip { get; set; }
        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }
    }
}