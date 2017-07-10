using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopParser.Infrastructure;
using ShopParser.Services;
using ShopParser.Interfaces;
namespace ShopParser.Tests.Services
{
    [TestFixture]
    class LinkParserTest
    {
        private ILinkParser _linkParser;
        private IDictionary<string, List<string>> _checkSet;
        [OneTimeSetUp]
        public void SetUp()
        {
            _linkParser=new CanUaLinkParser();
            _checkSet=new Dictionary<string, List<string>>();
            _checkSet.Add("https://can.ua/cpu/c1476/",new List<string>()
            {
                "https://can.ua/intel-core-i7-6700k-bx80662i76700k/p56378/",
                "https://can.ua/amd-fx-6350-fd6350frhkbox/p10821/"
            });
            _checkSet.Add("https://can.ua/wi-fi-adapters/c54366/", new List<string>()
            {
                "https://can.ua/asus-pce-ac88/p84618/",
                "https://can.ua/asus-pce-ac68/p37756/"
            });
        }
        [Test]
        public void GetProductLinksTest()
        {
            foreach (var category in _checkSet)
            {
                foreach (var product in category.Value)
                {
                    var links = _linkParser.GetProductLinks(category.Key);
                    Assert.AreEqual(links.Contains(product), true);
                }
            }
            
        }
        [Test]
        public void GetImageLinkTest()
        {
            foreach (var category in _checkSet)
            {
                foreach (var product in category.Value)
                {
                    var link = _linkParser.GetImageLink(product);
                    var pattern = "^(http|https)://.+.jpg";
                    var isMatch = Regex.Match(link, pattern).Success;
                    Assert.IsTrue(isMatch);
                }
            }
        }
        [Test]
        public void GetDescriptionTest()
        {
            foreach (var category in _checkSet)
            {
                foreach (var product in category.Value)
                {
                    var link = _linkParser.GetDescription(product);
                    Assert.Greater(link.Length,0);
                }
            }
        }
        [Test]
        public void GetNameTest()
        {
            foreach (var category in _checkSet)
            {
                foreach (var product in category.Value)
                {
                    var name = _linkParser.GetName(product);
                    Assert.Greater(name.Length, 0);
                }
            }
        }
        [Test]
        public void GePriceTest()
        {
            foreach (var category in _checkSet)
            {
                foreach (var product in category.Value)
                {
                    var price = _linkParser.GetPrice(product);
                    Assert.NotZero(price);
                }
            }
        }
    }
}
