using EZone.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
    public class Cart
    {
        [Key]
        public int OrderId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public virtual Product Product { get; set; }
    }
}
