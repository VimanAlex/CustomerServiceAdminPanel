using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Controllers
{
    public class ProductController : Controller
    {
        private ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService(new UnitOfWork(), this.ModelState);
        }

        // GET: Product
        public ActionResult Index()
        {
            var listProducts = _productService.GetAllProduct();

            return View(listProducts);
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product product)
        {
            _productService.AddProduct(product);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View("AddProduct",product);
            }

            
        }

        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index", "Product");
        }
    }
}