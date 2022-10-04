using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Customer_Service_Admin_Panel.Extension_Methods.AddCustomerAndAdressContext_Extension;

namespace Customer_Service_Admin_Panel.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerServiceEntity dbEntity;

        public CustomerRepository(CustomerServiceEntity context)
        {
            this.dbEntity = context;
        }

        public void Create(AddCustomerAndAdressContext add)
        {
            dbEntity.Customers.Add(add.CustomerModel());
                      
            dbEntity.Adresses.Add(add.AddressModel(add.CustomerModel()));
                     
            dbEntity.Contacts.Add(add.ContactModel(add.CustomerModel()));           
        }

        public AddCustomerAndAdressContext GetCustomer(int id)
        {
            var data = (from c in dbEntity.Customers
                        join a in dbEntity.Adresses on c.CustomerId equals a.CustomerId
                        join co in dbEntity.Contacts on c.CustomerId equals co.CustomerId
                        where c.CustomerId == id
                        select new
                        {
                            CustomerId = c.CustomerId,
                            OrganizationName = c.OrganizationName,
                            OrganizationNumber = c.OrganizationNumber,
                            Region = a.Region,
                            City = a.City,
                            StreetAddess1 = a.StreetAddess1,
                            StreetAddress2 = a.StreetAddress2,
                            ZipCode = a.ZipCode,
                            FirstName = co.FirstName,
                            LastName = co.LastName,
                            Mobile = co.Mobile,
                            Email = co.Email,
                            IsMain = co.IsMain,

                        }).AsEnumerable().Select(x => new AddCustomerAndAdressContext
                        {
                            CustomerId = x.CustomerId,
                            OrganizationName = x.OrganizationName,
                            OrganizationNumber = x.OrganizationNumber,
                            Region = x.Region,
                            City = x.City,
                            StreetAddess1 = x.StreetAddess1,
                            StreetAddress2 = x.StreetAddress2,
                            ZipCode = x.ZipCode,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Mobile = x.Mobile,
                            Email = x.Email,
                            IsMain = x.IsMain,
                        }).FirstOrDefault();



            return data;
        }

        public IEnumerable<Customer> GettAllCustomer()
        {
            var data = dbEntity.Customers.ToList();

            return data;

        }

        public void Save()
        {
            dbEntity.SaveChanges();
        }

        public void Update(AddCustomerAndAdressContext model , int id)
        {
            var customer = dbEntity.Customers.Where(x => x.CustomerId == id).SingleOrDefault();
            var address = dbEntity.Adresses.Where(x => x.CustomerId == id).FirstOrDefault();
            var contact = dbEntity.Contacts.Where(x => x.CustomerId == id).FirstOrDefault();

            var updateCustomer = model.CustomerModel(customer);
            var updateAddress = model.AddressModel(customer,address);
            var updateContact = model.ContactModel(customer,contact);

            dbEntity.Entry(updateCustomer).State = EntityState.Modified;
            dbEntity.Entry(updateAddress).State = EntityState.Modified;
            dbEntity.Entry(updateContact).State = EntityState.Modified;
        }
    }
}