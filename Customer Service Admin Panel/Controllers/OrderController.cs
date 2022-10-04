using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using Customer_Service_Admin_Panel.Repository;
using Customer_Service_Admin_Panel.Services.EmployeesService;
using Customer_Service_Admin_Panel.Services.OrderLineService;
using Customer_Service_Admin_Panel.Services.OrderService;
using Customer_Service_Admin_Panel.Services.OrderStatusService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Controllers
{
    public class OrderController : Controller
    {
        private OrderService _orderService;
        private EmployeesService _employeesService;
        private OrderLineService _orderLineService;
        private OrderStatusService _orderStatusService;
        private OrderTypeService _orderTypeService;

        public OrderController()
        {
            _orderService = new OrderService(new UnitOfWork(), this.ModelState);
            _employeesService = new EmployeesService(new UnitOfWork(), this.ModelState);
            _orderLineService = new OrderLineService(new UnitOfWork(), this.ModelState);
            _orderStatusService = new OrderStatusService(new UnitOfWork(), this.ModelState);
            _orderTypeService = new OrderTypeService(new UnitOfWork(), this.ModelState);
        }
        
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddOrder(int id)
        {
           
            ViewBag.ListOrderType = _orderTypeService.GetAllOrderType().ToList();
           
            ViewBag.ListOrderStatus = _orderStatusService.GetAllOrderStatus().ToList();

            var model = new AddOrder();
            model.CustomerId = id;
            model.ListOfEmployees = _employeesService.GetAllEmployee(id).ToList();

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult AddOrder(AddOrder model , int id)
        {

            _orderService.CreateOrder(model, id);

            if (ModelState.IsValid)
            {
                
                return RedirectToAction("DetailsCustomer", "Customer", new { id = id });
            }
            else
            {               
                ViewBag.ListOrderType = _orderTypeService.GetAllOrderType().ToList();

                ViewBag.ListOrderStatus = _orderStatusService.GetAllOrderStatus().ToList();

                return View("AddOrder", model);
            }

                
        }

        public ActionResult UpdateOrder(int id,int orderId)
        {
            var order = _orderService.GetOrder(id, orderId);

            
            ViewBag.OrderTypeList = _orderTypeService.GetSelectListOrderType();           
            ViewBag.StatusOrderList =_orderStatusService.GetSelectListOrderStatus();

           
            ViewBag.ListOrderLine = _orderLineService.GetSelectListOrderLine(id,orderId);


           
            return View(order);

        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult UpdateOrder(int id , AddOrder model)
        {
                _orderService.UpdateOrder(id, model);
                return RedirectToAction("DetailsCustomer", "Customer", new { id = id });  
        }

        public ActionResult DeleteOrder(int id,int orderId)
        {
            if (id != null)
            {                              
                _orderService.DeleteOrder(id, orderId);
                return RedirectToAction("DetailsCustomer", "Customer", new { id = id });
            }
            return View();
            
        }

    }
}