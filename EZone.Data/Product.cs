using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Display(Name ="Product Name")]
        [MinLength(2,ErrorMessage ="Enter at lease 2 characters")]
        [MaxLength(100,ErrorMessage ="Too long characters. Please enter less than 100 characters.")]
        public string ProductName { get; set; }
              
        [Display(Name ="Created Date")]
        public DateTimeOffset CreatedDate { get; set; }
        [Display(Name ="Modified Date")]
        public DateTimeOffset? ModifiedDate { get; set; }
        public string Description { get; set; }
        [Display(Name ="Product Image")]
        public string ProductImage { get; set; }
        [Display(Name ="Is New")]
        public bool IsNew { get; set; }
        [Required]
        //[Range(typeof(int),"1", "10000000000", ErrorMessage ="Invalid Quantity")]
        public int Quantity { get; set; }
        [Required]
       // [Range(typeof(double),"1", "10000000000", ErrorMessage ="Invalid Price")]
        public double Price { get; set; }
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
      //  [ForeignKey("CategoryId")]
        public virtual List<Category> Category { get; set; }

    }
}
