using EZone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
    public class MultipleDisplay
    {
        public List<Order> ListA { get; set; }
        public List<OrderDetail> ListB { get; set; }
    }
}
