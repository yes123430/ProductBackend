using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductBackend.Models
{
    public class ShowShopIndexModel
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string ProductID { get; set; }
    }
}