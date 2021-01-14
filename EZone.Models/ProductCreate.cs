using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
   public  class ProductCreate
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name ="Product Name")]
        [MinLength(2,ErrorMessage ="Enter at lease 2 characters")]
        [MaxLength(100, ErrorMessage ="Too long characters")]
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
       
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [Display(Name ="Is New")]
        public bool IsNew { get; set; }
    }
}
