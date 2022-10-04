using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Extension_Methods.Employees_Extensions
{
    public static class EmployeeExtensions
    {
        public static Employee EmployeeModel(this Employee employeeModel, Customer customer,Employee employee = null)
        {
            if(employee == null)
            {
                employee = new Employee();
            }

            employee.Customer = customer;
            employee.FirstName = employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;

            return employee;
        }
    }
}