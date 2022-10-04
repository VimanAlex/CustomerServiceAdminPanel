using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.Extension_Methods.Employees_Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private CustomerServiceEntity dbEntity;

        public EmployeesRepository(CustomerServiceEntity context)
        {
            this.dbEntity = context;
        }

        public void AddEmployee(Employee model, int id)
        {          
            Customer customer = dbEntity.Customers.Find(id);
           
              if (customer != null)
              {                                  
                   dbEntity.Employees.Add(model.EmployeeModel(customer));                
              }
        }

        public void DeleteEmployee(int employeeId, int id)
        {
            var customer = dbEntity.Customers.Find(id);
            var employee = dbEntity.Employees.Where(x => x.CustomerId == customer.CustomerId && x.EmployeeId == employeeId).First();

            if(customer != null)
            {
                dbEntity.Employees.Remove(employee);
            }
        }

        public IEnumerable<Employee> GetAllEmployee(int id)
        {
            var data = dbEntity.Employees.Where(x=>x.CustomerId == id).ToList();

            return data;
        }

        public void Save()
        {
            dbEntity.SaveChanges();
        }
    }
}