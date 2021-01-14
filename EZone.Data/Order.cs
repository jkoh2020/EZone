using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EZone.Data
{
    [Bind(Exclude = "OrderId")]
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public string UserName { get; set; }
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "Enter at least 2 Characters")]
        [MaxLength(50, ErrorMessage = "Too long characters")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "Enter at least 2 Characters")]
        [MaxLength(50, ErrorMessage = "Too long characters")]
        public string LastName { get; set; }
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        public int ZipCode { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        [ScaffoldColumn(false)]
        public DateTimeOffset OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }


    }
}
