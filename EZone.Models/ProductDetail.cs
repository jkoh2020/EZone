using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
    public class ProductDetail
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Category Id")]
        public Nullable <int> CategoryId { get; set; }
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
       
        [Display(Name ="Product Name")]
        [MinLength(2,ErrorMessage ="Enter at lease 2 characters")]
        [MaxLength(100, ErrorMessage ="Too long characters")]
        public string ProductName { get; set; }
             
        public string Description { get; set; }
        [Display(Name ="Image Name")]
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Is New")]
        public bool IsNew { get; set; }
        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedDate { get; set; }
        [Display(Name = "Date Modified")]
        public DateTimeOffset? ModifiedDate { get; set; }

    }
}
