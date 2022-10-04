using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using Customer_Service_Admin_Panel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Services
{
    public class CustomerService : ICustomerServices 
    {
        private ModelStateDictionary _modelState;
        private UnitOfWork _unitOfWork;
        private CustomerValidation CustomerValidation = new CustomerValidation();
        
        public CustomerService(UnitOfWork unitOfWork, ModelStateDictionary modelState)
        {
            this._modelState = modelState;
            this._unitOfWork = unitOfWork;
        }


        public void CreateCustomer(AddCustomerAndAdressContext customer)
        {
            if (CustomerValidation.IsValid(customer,_modelState))
            {
                try
                {
                    _unitOfWork.CustomerRepository.Create(customer);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    throw new HttpException(ex.Message);

                }
            }

        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var getCustomers = _unitOfWork.CustomerRepository.GettAllCustomer();

            return getCustomers;
        }

        public AddCustomerAndAdressContext GetCustomer(int id)
        {
            var getCustomer = _unitOfWork.CustomerRepository.GetCustomer(id);

            return getCustomer;
        }

        public void UpdateCustomer(AddCustomerAndAdressContext model, int id)
        {
            _unitOfWork.CustomerRepository.Update(model,id);
            _unitOfWork.Save();
        }
    }
}