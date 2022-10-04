using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using Customer_Service_Admin_Panel.Repository;
using Customer_Service_Admin_Panel.Services;
using Customer_Service_Admin_Panel.Services.ContractService;
using Customer_Service_Admin_Panel.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Controllers
{
    public class ContractController : Controller
    {
        private CustomerService _customerService;
        private ContractService _contractService;
        private ProductService _productService;

        public ContractController()
        {
            _customerService = new CustomerService(new UnitOfWork(),this.ModelState);
            _contractService = new ContractService(new UnitOfWork(),this.ModelState);
            _productService = new ProductService(new UnitOfWork(),this.ModelState);
        }
     
        public ActionResult AddContract(int id)
        {          
            var model = new AddContract();
            model.CustomerId = id;

            if (id != null)
            {                
                ViewBag.ListProduct = _productService.GetAllProduct().ToList();
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddContract(AddContract model, int id)
        {                    
            _contractService.AddContract(model, id);

            if (ModelState.IsValid)
            {
                return RedirectToAction("DetailsCustomer", "Customer", new { id = id });
            }
            else
            {
                return View("AddCustomer", model);
            }
        }

        public ActionResult UpdateContract(int id, int contractId)
        {            
            var contract = _contractService.GetContract(id,contractId);

            ViewBag.ListProduct = _productService.GetAllProduct().ToList();

            return View(contract);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult UpdateContract(AddContract model,int id)
        {           
             _contractService.UpdateContract(model, id);

            if (ModelState.IsValid)
            {
                return RedirectToAction("DetailsCustomer", "Customer", new { id = id });
            }
            else
            {
                return View("UpdateContract", model);
            }
        }

        
        public ActionResult DeleteContract(int id , int contractId)
        {
           if(id != null)
            {                           
                _contractService.DeleteContract(id, contractId);
                return RedirectToAction("DetailsCustomer","Customer", new {id=id});
            }
            return View();
        }
    }
}