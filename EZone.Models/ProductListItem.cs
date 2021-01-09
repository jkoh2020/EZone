using EZone.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
    public class ProductListItem
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Category ID")]
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
