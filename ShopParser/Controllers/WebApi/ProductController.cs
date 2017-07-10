using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using ShopParser.Interfaces;
using ShopParser.Models;

namespace ShopParser.Controllers.WebApi
{
    public class ProductController : ApiController
    {
        public IParsingService ParsingService;
        public ProductController(IParsingService parsingService)
        {
            ParsingService = parsingService;
        }

        public IHttpActionResult GetAll()
        {
            try
            {
                var result = ParsingService.GetAll();
                return Json(result);
            }
            catch
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult GetById(int id)
        {
            try
            {
                var result = ParsingService.GetById(id);
                return Json(result);
            }
            catch
            {
                return InternalServerError();
            }
            
        }
        [HttpPost]
        [Route("api/Product/Parse/")]
        public async Task<IHttpActionResult> Parse([FromBody]ParseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await ParsingService.ParseNewAsync(model.RequestString);
                    return Json(result);
                }
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("api/Product/Refresh/")]
        public async Task<IHttpActionResult> Refresh()
        {
            try
            {
                var result = await ParsingService.RefreshAsync();
                return Json(result);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
