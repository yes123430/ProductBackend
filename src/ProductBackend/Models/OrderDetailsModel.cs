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
        [Required]
        public int ID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int Item { get; set; }
    }
}