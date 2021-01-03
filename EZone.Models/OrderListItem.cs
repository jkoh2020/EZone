using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
    public class OrderListItem
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public double OrderTotal { get; set; }
        public bool IsShipped { get; set; }
        public DateTimeOffset DateOfOrder { get; set; }
        public DateTimeOffset? DateOfShipping { get; set; }
        public virtual List<ProductListItem> ProuductList { get; set; }
        

    }
}
