using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.Repository;
using Customer_Service_Admin_Panel.Services.EmployeesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeesService _employeesService;

        public EmployeesController()
        {
            _employeesService = new EmployeesService(new UnitOfWork(), this.ModelState);
        }

        public ActionResult CreateEmployee(int id)
        {           
            ViewBag.ListEmployee = _employeesService.GetAllEmployee(id).ToList();

            var model = new Employee();
            model.CustomerId = id;

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(Employee model , int id)
        {
            if (id != null)
            {                 
                _employeesService.AddEmployee(model, id);
                if (ModelState.IsValid)
                {
                    return RedirectToAction("CreateEmployee", "Employees", new { id = id });
                }
                else
                {
                    ViewBag.ListEmployee = _employeesService.GetAllEmployee(id).ToList();

                    return View("CreateEmployee", model);
                }
               
                
            }

            return View(model);
        }

        public ActionResult DeleteEmployee(int employeeId,int id)
        {
            if(id != null)
            {                                
                _employeesService.DeleteEmployee(employeeId, id);

                return RedirectToAction("CreateEmployee", "Employees", new {id=id});
            }
            return View();
        }

    }
}