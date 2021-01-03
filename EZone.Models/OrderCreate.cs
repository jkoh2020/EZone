using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
    public class OrderCreate
    {
        public int OrderId { get; set; }
       
        public int ProductId { get; set; }
       
        public int OrderQuantity { get; set; }
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "Enter at least 2 Characters")]
        [MaxLength(50, ErrorMessage = "Too long characters")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "Enter at least 2 Characters")]
        [MaxLength(50, ErrorMessage = "Too long characters")]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public DateTimeOffset? DateOfOrder { get; set; }
    }
}
