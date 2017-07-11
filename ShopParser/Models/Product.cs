using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace ShopParser.Models
{
    [JsonObject(IsReference = true)]
    public class Product
    {
        public int Id { get; set; }
        public string ProductLink { get; set; }
        public string ImageLink { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataMember]
        public virtual IList<PriceHistory> PriceHistory { get; set; }

        public Product()
        {
            PriceHistory=new List<PriceHistory>();
        }
    }
}