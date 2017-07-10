using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using ShopParser.Models;
using ShopParser.Interfaces;
namespace ShopParser.Infrastructure
{
    public class CanUaLinkParser: ILinkParser
    {
        private readonly HtmlWeb _webInstance;

        public CanUaLinkParser()
        {
            _webInstance=new HtmlWeb();
        }
        public IEnumerable<string> GetProductLinks(string catalogLink)
        {
            var doc = _webInstance.Load(catalogLink);
            var links = doc.DocumentNode.QuerySelectorAll(".item .image a")
                                        .Select(n=>n.Attributes["href"].Value);
            return links;
        }

        
        public string GetImageLink(string productLink)
        {
            var docRoot = _webInstance.Load(productLink).DocumentNode;
            return docRoot.QuerySelector(".product-photo img")
                          .Attributes["src"]
                          .Value;
        }
        public string GetDescription(string productLink)
        {
            var docRoot = _webInstance.Load(productLink).DocumentNode;
            return docRoot.QuerySelector(".info-product")
                          .InnerText;
        }
        public string GetName(string productLink)
        {
            var docRoot = _webInstance.Load(productLink).DocumentNode;
            return docRoot.QuerySelector("#goods_title")
                          .InnerText;
        }
        public double GetPrice(string productLink)
        {         
            var docRoot = _webInstance.Load(productLink).DocumentNode;
            var price = docRoot.QuerySelector(".price")
                               .InnerText;
            var resultString = new Regex(@"[^\d]").Replace(price,string.Empty);
            return Convert.ToDouble(resultString);
        }
    }
}