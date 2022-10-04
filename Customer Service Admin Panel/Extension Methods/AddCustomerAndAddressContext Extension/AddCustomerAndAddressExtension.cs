using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;

namespace Customer_Service_Admin_Panel.Extension_Methods.AddCustomerAndAdressContext_Extension
{
    public static class AddCustomerAndAddressContextExtension
    {
        public static Customer CustomerModel(this AddCustomerAndAdressContext customerModel, Customer customer = null)
        {
            if (customer == null)
            {
                customer = new Customer();
            }

            customer.OrganizationNumber = customerModel.OrganizationNumber;
            customer.OrganizationName = customerModel.OrganizationName;

            return customer;
        }


        public static Adress AddressModel(this AddCustomerAndAdressContext addressModel, Customer customer, Adress adress = null)
        {  
            if (adress == null)
            {
                adress = new Adress();
            }
            
            adress.Customer = customer;
            adress.StreetAddess1 = addressModel.StreetAddess1;
            adress.StreetAddress2 = addressModel.StreetAddress2;
            adress.Region = addressModel.Region;
            adress.City = addressModel.City;
            adress.ZipCode = addressModel.ZipCode;

            return adress;
    
        }

        public static Contact ContactModel (this AddCustomerAndAdressContext contactModel , Customer customer, Contact contact = null)
        {
            if(contact == null)
            {
                contact = new Contact();
            }

            contact.Customer = customer;
            contact.FirstName = contactModel.FirstName;
            contact.LastName = contactModel.LastName;
            contact.Mobile = contactModel.Mobile;
            contact.Email = contactModel.Email;
            contact.IsMain = contactModel.IsMain;

            return contact;
        }


        //return context.tblClients.Where(cli => cli.ClientName.Contains(clientName)).ToList();

      public static Customer UpdateCustomer(this AddCustomerAndAdressContext customerModel,Customer updateCustomer)
        {
           
            updateCustomer.OrganizationNumber = customerModel.OrganizationNumber;
            updateCustomer.OrganizationName = customerModel.OrganizationName;

            return updateCustomer;
        }

      public static Adress UpdateAddress(this AddCustomerAndAdressContext addressModel , Adress updateAddress , Customer customer)
        {
            
            updateAddress.Customer = customer;
            updateAddress.StreetAddess1 = addressModel.StreetAddess1;
            updateAddress.StreetAddress2 = addressModel.StreetAddress2;
            updateAddress.Region = addressModel.Region;
            updateAddress.City = addressModel.City;
            updateAddress.ZipCode = addressModel.ZipCode;

            return updateAddress;

        }

    }
}