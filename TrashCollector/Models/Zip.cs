using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Zip
    {
        [Key]
        public int ZipId { get; set; }
        public int ZipCode { get; set; }
    }
}