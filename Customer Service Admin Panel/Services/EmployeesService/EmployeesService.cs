using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Services.EmployeesService
{
    public class EmployeesService : IEmployeesService
    {
        private UnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;
        private EmployeesValidation EmployeesValidation = new EmployeesValidation();

        public EmployeesService(UnitOfWork unitOfWork, ModelStateDictionary modelState)
        {
            _unitOfWork = unitOfWork;
            _modelState = modelState;
        }

        public void AddEmployee(Employee employee, int id)
        {
            if (EmployeesValidation.IsValid(employee,_modelState,id))
            {
                _unitOfWork.EmployeesRepository.AddEmployee(employee, id);
                _unitOfWork.Save();
            }
            
        }

        public void DeleteEmployee(int employeeId, int id)
        {
            _unitOfWork.EmployeesRepository.DeleteEmployee(employeeId, id);
            _unitOfWork.Save();
        }

        public IEnumerable<Employee> GetAllEmployee(int id)
        {
           var employees = _unitOfWork.EmployeesRepository.GetAllEmployee(id);

           return employees;               
        }
    }
}