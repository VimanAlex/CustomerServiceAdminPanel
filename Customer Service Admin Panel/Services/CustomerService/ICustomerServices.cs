using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using Customer_Service_Admin_Panel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Services
{
    public interface ICustomerServices
    { 
        void CreateCustomer(AddCustomerAndAdressContext customer);
        IEnumerable<Customer> GetAllCustomers();

        AddCustomerAndAdressContext GetCustomer(int id);
        void UpdateCustomer(AddCustomerAndAdressContext model, int id);
    }
}
