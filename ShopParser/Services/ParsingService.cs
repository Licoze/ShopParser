using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ShopParser.Interfaces;
using ShopParser.Models;

namespace ShopParser.Services
{
    public class ParsingService:IParsingService
    {
        private readonly ILinkParser _parser;
        private readonly ProductsContext _db;
        public ParsingService(ILinkParser parser):this(parser,new ProductsContext())
        {            
        }

        public ParsingService(ILinkParser parser, ProductsContext db)
        {
            _parser = parser;
            _db = db;
        }
        public async Task<int> ParseNewAsync(string link)
        {
            var productList = _parser.GetProductLinks(link);
            foreach (var productLink in productList)
            {
                bool isNew = false;
                var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductLink == productLink);
                if (product == null)
                {
                    isNew = true;
                    product= new Product()
                    {
                        ProductLink = productLink
                    };
                }

                product=FillInfo(product, productLink);
                if (isNew == true)
                {
                    _db.Products.Add(product);
                }
                
            }
            return await _db.SaveChangesAsync();
        }

        public async Task<int> RefreshAsync()
        {
            var products = _db.Products;
            foreach (var product in products)
            {
               var updated=FillInfo(product, product.ProductLink);
                bool isNull = updated.GetType().GetProperties().Where(q=>q.PropertyType==typeof(string))
                    .Any(p => p.GetValue(updated) == string.Empty);
                if (isNull)
                    _db.Products.Remove(product);
            }
            return await _db.SaveChangesAsync();
        }

        public Product GetById(int id)
        {
            return _db.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _db.Products;
            products.Load();
            return products;
        }

        private Product FillInfo(Product product, string productLink)
        {
            product.ImageLink = _parser.GetImageLink(productLink);
            product.Name = _parser.GetName(productLink);
            product.Description = _parser.GetDescription(productLink);
            var lastPrice = product.PriceHistory.LastOrDefault()?.Price;
            var currentPrice = _parser.GetPrice(productLink);
            if (lastPrice != currentPrice)
            {
                product.PriceHistory.Add(new PriceHistory()
                {
                   Date = DateTime.Now,
                   Price = currentPrice
                });
            }
            return product;
        }
    }
}