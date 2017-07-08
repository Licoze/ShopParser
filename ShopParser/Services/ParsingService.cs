using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using ShopParser.Models;
using ShopParser.Interfaces;
namespace ShopParser.Services
{
    public class ParsingService: IParsingService
    {

        public IEnumerable<string> GetProductLinks(string catalogLink)
        {
            var web =new HtmlWeb();
            var doc = web.Load(catalogLink);
            var links = doc.DocumentNode.QuerySelectorAll(".item .image a")
                                        .Select(n=>n.Attributes["href"].Value);
            return links;
        }

        
        public string GetImageLink(string productLink)
        {
            var web = new HtmlWeb();
            var docRoot = web.Load(productLink).DocumentNode;
            return docRoot.QuerySelector(".product-photo img")
                          .Attributes["src"]
                          .Value;
        }
        public string GetDescription(string productLink)
        {
            var web = new HtmlWeb();
            var docRoot = web.Load(productLink).DocumentNode;
            return docRoot.QuerySelector(".info-product")
                          .InnerText;
        }
        public string GetName(string productLink)
        {
            var web = new HtmlWeb();
            var docRoot = web.Load(productLink).DocumentNode;
            return docRoot.QuerySelector("#goods_title")
                          .InnerText;
        }
        public string GetPrice(string productLink)
        {
            var web = new HtmlWeb();
            var docRoot = web.Load(productLink).DocumentNode;
            return docRoot.QuerySelector(".price")
                           .InnerText;
        }
    }
}