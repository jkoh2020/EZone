﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models
{
   public class UserDetail
    {
        [Display(Name ="User ID")]
        public string UserId { get; set; }
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        //[Display(Name ="Status")]
        //public bool IsAdmin { get; set; }

    }
}
