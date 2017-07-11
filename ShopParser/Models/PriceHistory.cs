using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace ShopParser.Models
{
    
    public class PriceHistory
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public double Price { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}