using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using ShopParser.Controllers.WebApi;
using ShopParser.Infrastructure;
using ShopParser.Interfaces;
using ShopParser.Models;
using ShopParser.Services;

namespace ShopParser.Tests.Controllers
{
    [TestFixture]
    class ProductControllerTest
    {
        private ILinkParser _parser;        
        private ProductsContext _db;
        private IParsingService _service;
        [OneTimeSetUp]
        public void SetUp()
        {
            _parser=new CanUaLinkParser();
            _db=new ProductsContext();
            _service =new ParsingService(_parser, _db);
        }
        [Test]
        public async Task ParseTest()
        {
            var controller=new ProductsController(_service);
            var checkLink = "https://can.ua/cpu/c1476/";
            var expected = await _service.ParseNewAsync(checkLink);
            var actual = await controller.Parse(checkLink) as JsonResult<int>;            
            Assert.AreEqual(expected,actual?.Content);
        }
        [Test]
        public async Task RefreshTest()
        {
            var controller = new ProductsController(_service);
            var expected = await _service.RefreshAsync();
            var actual = await controller.Refresh() as JsonResult<int>;
            Assert.AreEqual(expected, actual?.Content);
        }
        [Test]
        public void GetByIdTest()
        {
            var controller = new ProductsController(_service);
            var products = _db.Products;
            foreach (var product in products)
            {
                var expected = _service.GetById(product.Id);
                var actual = controller.GetById(product.Id) as JsonResult<Product>;
                Assert.AreEqual(expected, actual?.Content);
            }

        }
        [Test]
        public void GetAllTest()
        {
            var controller = new ProductsController(_service);
            var expected = _db.Products;
            expected.Load();
            var actual = controller.GetAll() as JsonResult<IEnumerable<Product>>;
            CollectionAssert.AreEquivalent(expected, actual?.Content);
        }
    }
}
