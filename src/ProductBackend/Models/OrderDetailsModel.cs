using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductBackend.Models
{
    public class OrderDetailsModel
    {
        
        [Key]
        public int ID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public string Item { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}