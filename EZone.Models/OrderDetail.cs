using EZone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OrderQuantity { get; set; }
        public double Price { get; set; }
        public double OrderTotal { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        


    }
}
