using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShopParser.Interfaces;

namespace ShopParser.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Parser
        
        public IParsingService ParsingService;
        public ProductsController(IParsingService parsingService)
        {
            ParsingService = parsingService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            try
            {
                return PartialView("Partial/Products"); 
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult ProductDetails()
        {
            try
            {
                return PartialView("Partial/ProductDetails");
            }
            catch
            {
                return View("Error");
            }

        }

    }
}