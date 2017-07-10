using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopParser.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductLink { get; set; }
        public string ImageLink { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<PriceHistory> PriceHistory { get; set; }

        public Product()
        {
            PriceHistory=new List<PriceHistory>();
        }
    }
}