using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShopParser.Interfaces;

namespace ShopParser.Controllers
{
    public class ProductController : Controller
    {
        // GET: Parser
        
        public IParsingService ParsingService;
        public ProductController(IParsingService parsingService)
        {
            ParsingService = parsingService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            try
            {
                var result = ParsingService.GetAll();
                return PartialView("Partial/Products", result); 
            }
            catch
            {
                return View("Error");
            }
        }
        [Route("Product/{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var result = ParsingService.GetById(id);
                return PartialView("Partial/ProductDetails",result);
            }
            catch
            {
                return View("Error");
            }

        }

    }
}