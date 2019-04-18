using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCEmail.Models
{
    public class EmailFormModel
    {

        [Required, Display(Name = "Please entry Your Email")]
        public string FromEmail { get; set; }
        [Display(Name ="Plase Enty Your Code")]
        public string EntryCode {get;set;}
    }
}