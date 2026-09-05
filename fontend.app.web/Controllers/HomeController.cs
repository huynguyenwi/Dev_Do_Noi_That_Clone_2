using System.Diagnostics;
using core.model;
using CRM.data;
using csdl.web.Services;
using fontend.app.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace fontend.app.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService_I _productService;
        public HomeController(ProductService_I productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new HomeModel();
            model.arrProductsHome = await _productService.GetProductsHome();
            return View("Index", model);
        }

    }
}
