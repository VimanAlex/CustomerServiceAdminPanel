using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.ModelView;
using Customer_Service_Admin_Panel.Repository;
using Customer_Service_Admin_Panel.Services;
using Customer_Service_Admin_Panel.Services.ContractService;
using Customer_Service_Admin_Panel.Services.OrderService;
using Customer_Service_Admin_Panel.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Customer_Service_Admin_Panel.Controllers
{
    public class CustomerController : Controller
    {
     
        private CustomerService _serviceCustomer;
        private ContractService _serviceContract;
        private OrderService _serviceOrder;
        public CustomerController()
        {
            _serviceCustomer = new CustomerService(new UnitOfWork(), this.ModelState);
            _serviceContract = new ContractService(new UnitOfWork(),this.ModelState);
            _serviceOrder = new OrderService(new UnitOfWork(), this.ModelState);
        }
       
        // GET: Customer
        [Authorize]
        public ActionResult Index()
        {
            var listCustomers = _serviceCustomer.GetAllCustomers();

            return View(listCustomers);
        }

        [Authorize]
        public ActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(AddCustomerAndAdressContext model)
        {
            _serviceCustomer.CreateCustomer(model);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("CreateCustomer",model);
            }            
        }

        [Authorize]
        public ActionResult DetailsCustomer(int id)
        {
          List<AddCustomerAndAdressContext> list = new List<AddCustomerAndAdressContext>();        
            var data = _serviceCustomer.GetCustomer(id);
             list.Add(data);

            List<AddContract> listContract = new List<AddContract>();
            var getContract = _serviceContract.GetAllContracts(id);
            listContract.AddRange(getContract);

            ViewBag.ListContract = listContract;

            List<AddOrder> orderList = new List<AddOrder>();
            var getOrders = _serviceOrder.GetAllOrders(id);
            orderList.AddRange(getOrders);
            ViewBag.ListOrders = orderList;

            return View(list);
        }

        [Authorize]
        public ActionResult UpdateCustomer(int id)
        {
            var data = _serviceCustomer.GetCustomer(id);

            return View(data);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(AddCustomerAndAdressContext model , int id)
        {
            _serviceCustomer.UpdateCustomer(model,id);
            return RedirectToAction("DetailsCustomer", new { id = id });
            
        }
    }
}