using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopParser.Models
{
    public class ParseViewModel
    {
        [Required]
        public string RequestString { get; set; }
    }
}