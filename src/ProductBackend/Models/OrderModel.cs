using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductBackend.Models
{
    public class OrderModel
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string OrderMail { get; set; }
       
        public virtual ICollection<Models.OrderDetailsModel> OrderDetailModels { get; set; } 
    }
}