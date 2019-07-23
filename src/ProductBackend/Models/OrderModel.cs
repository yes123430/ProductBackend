using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductBackend.Models
{
    public class OrderModel
    {
        [Key]
        [Required]
        
        public int ID { get; set; }
        [Display(Name ="訂購日期")]
        [Required]
        public DateTime OrderDate { get; set; }
        [Display(Name = "郵件")]
        [Required]
        public string OrderMail { get; set; }

        [Display(Name = "總價")]
        public double Amount { get; set; }

        [Display(Name = "備註")]
        public string Description { get; set; }

        [ForeignKey("OrderID")]
        public virtual ICollection<Models.OrderDetailsModel> OrderDetailModels { get; set; } 
    }
}