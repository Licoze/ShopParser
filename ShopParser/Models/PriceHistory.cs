using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopParser.Models
{
    public class PriceHistory
    {     
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public virtual Product Product { get; set; }
    }
}