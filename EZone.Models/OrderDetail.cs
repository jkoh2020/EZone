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
        public int OrderId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public double OrderTotal { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsShipped { get; set; }
        public DateTimeOffset? DateOfShipped { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }


    }
}
