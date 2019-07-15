using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductBackend.Models
{
    public class ProductModel
    {    
        [Key]
        [Required]
        public int ID { get; set; }

        [Display(Name ="產品名稱")]
        [Required]
        public string ProdName { get; set; }

        [Display(Name = "圖片")]
        public string ImagePath { get; set; }

        [Display(Name = "價格")]
        [Required]
        public double Price { get; set; }

        [Display(Name = "庫存量")]
        public double Count { get; set; }
    }
}