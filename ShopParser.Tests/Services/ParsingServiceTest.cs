using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopParser.Infrastructure;
using ShopParser.Interfaces;
using ShopParser.Models;
using ShopParser.Services;

namespace ShopParser.Tests.Services
{
    [TestFixture]
    class ParsingServiceTest
    {
        private IParsingService _service;
        private ProductsContext _db;
        [OneTimeSetUp]
        public void SetUp()
        {
            _db = new ProductsContext();
            _service =new ParsingService(new CanUaLinkParser(), _db);
            
        }
        [Test]
        public async Task ParseNewTest()
        {
            var result =await _service.ParseNewAsync("https://can.ua/cpu/c1476/");
            Assert.GreaterOrEqual(result,0);
        }

        [Test]
        public async Task RefreshTest()
        {
            var product = _db.Products.FirstOrDefault();
            var prices = product?.PriceHistory;
            prices.Add(new PriceHistory()
            {
                Date = DateTime.Now,
                Price = 0
            });
            await _db.SaveChangesAsync();
            var result = await _service.RefreshAsync();
            Assert.Greater(result,0);
        }
        [Test]
        public void GetByIdTest()
        {
            var expected = _db.Products.FirstOrDefault();
            var actual = _service.GetById(expected.Id);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetAllTest()
        {
            var expected = _db.Products;
            expected.Load();;
            var actual = _service.GetAll();
            CollectionAssert.AreEquivalent(expected,actual);
        }
    }
}
