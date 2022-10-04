using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Validation
{
    public  class CustomerValidation : IValidation<AddCustomerAndAdressContext>
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public  bool IsValid(AddCustomerAndAdressContext customerToValidate, ModelStateDictionary _modelState)
        {
          
            
                if (String.IsNullOrEmpty(customerToValidate.FirstName))
                {
                    _modelState.AddModelError("FirstName", "First Name field must contain minimum 1 letter ");
                }

                if (String.IsNullOrEmpty(customerToValidate.LastName))
                {
                    _modelState.AddModelError("LastName", "Last Name field must contain minimum 1 letter ");

                }

                if (customerToValidate.Mobile.ToString().Length < 9)
                {
                    _modelState.AddModelError("Mobile", "Incorect Number Mobile! Must have minim 10 numbers");
                }

                if (String.IsNullOrEmpty(customerToValidate.Email))
                {

                    _modelState.AddModelError("Email", "Incorect Format Mail");

                }

                if (customerToValidate.OrganizationNumber.ToString().Length > 0)
                {
                    var isOrgNumber = _unitOfWork.CustomerRepository
                        .GettAllCustomer()
                        .Any(x => x.OrganizationNumber == customerToValidate.OrganizationNumber);

                    if (isOrgNumber)
                    {
                        _modelState.AddModelError("OrganizationNumber", "Organization Number Already Exist ");
                    }

                }

                if (String.IsNullOrEmpty(customerToValidate.OrganizationName))
                {

                    _modelState.AddModelError("OrganizationName", "Organization Name field must contain minimum 1 letter ");

                }

                if (String.IsNullOrEmpty(customerToValidate.Region))
                {
                    _modelState.AddModelError("Region", "Region must completed");
                }

                if (String.IsNullOrEmpty(customerToValidate.City))
                {
                    _modelState.AddModelError("City", "City must completed");
                }

                if (String.IsNullOrEmpty(customerToValidate.StreetAddess1))
                {
                    _modelState.AddModelError("StreetAddess1", "StreetAddess1 must completed");
                }

                if (customerToValidate.ZipCode.ToString().Length < 4)
                {
                    _modelState.AddModelError("ZipCode", "ZipCode must have minim 5 cifres");
                }


                return _modelState.IsValid;
            
        }

        public bool IsValid(AddCustomerAndAdressContext entity, ModelStateDictionary modelState, int id)
        {
            throw new NotImplementedException();
        }
    }
}