using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Data
{
    public class Category
    {
        [Key]
       
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [MinLength(2, ErrorMessage ="Enter at least 2 characters")]
        [MaxLength(100, ErrorMessage ="Too long characters")]
        public string CategoryName { get; set; }
    }
}
