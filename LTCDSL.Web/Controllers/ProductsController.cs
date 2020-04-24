using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    using BLL;
    using DAL.Models;
    using Common.Req;
    using System.Collections.Generic;
    //using BLL.Req;
    using Common.Rsp;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
            _svc = new ProductsSvc();
        }

        [HttpPost("get-all-products")]
        public IActionResult getAllProducts()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("search-product")]
        public IActionResult SearchProduct([FromBody]SearchProductReq req)
        {
            var res = new SingleRsp();
            var products = _svc.SearchProduct(req.Page, req.Size, req.Id, req.Type, req.Keyword);
            res.Data = products;
            return Ok(res);
        }

        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody]ProductReq req)
        {
            var res = _svc.CreateProduct(req);
            return Ok(res);
        }

        [HttpPost("update-product")]
        public IActionResult UpdateProduct([FromBody]ProductReq req)
        {
            var res = _svc.UpdateProduct(req);
            return Ok(res);
        }

        [HttpPost("delete-product")]
        public IActionResult DeleteProduct([FromBody]ProductReq req)
        {
            var res = _svc.DeleteProduct(req.ProductId);
            return Ok(res);
        }

        private readonly ProductsSvc _svc;
    }
}