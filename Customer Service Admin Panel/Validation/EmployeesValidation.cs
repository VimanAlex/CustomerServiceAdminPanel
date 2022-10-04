using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Validation
{
    public class EmployeesValidation : IValidation<Employee>
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public bool IsValid(Employee employeeValidation, ModelStateDictionary modelState,int id)
        {
            if (String.IsNullOrEmpty(employeeValidation.FirstName))
            {
                modelState.AddModelError("FirstName", "Employee must have First Name completed");
            }
            if (String.IsNullOrEmpty(employeeValidation.LastName))
            {
                modelState.AddModelError("LastName", "Employee must have Last Name completed");
            }

            if (employeeValidation.FirstName.ToString().Length > 0 && employeeValidation.LastName.Length >0)
            {
                var isExistEmployee = _unitOfWork.EmployeesRepository
                    .GetAllEmployee(id)
                    .Any(x => x.FirstName == employeeValidation.FirstName && x.LastName == employeeValidation.LastName);

                      if (isExistEmployee)
                      {
                          modelState.AddModelError("FirstName", "This Employee Already Exist ");
                          modelState.AddModelError("LastName", "This Employee Already Exist ");
                      }

            }

            if (!employeeValidation.IsChecked)
            {
                modelState.AddModelError("IsChecked", "Select minim one Employee");
            }

            return modelState.IsValid;
        }

        public bool IsValid(Employee entity, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }
    }
}