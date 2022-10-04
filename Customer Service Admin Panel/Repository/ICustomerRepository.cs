using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Repository
{
    internal interface ICustomerRepository
    {
       IEnumerable<Customer> GettAllCustomer();
       AddCustomerAndAdressContext GetCustomer(int id);
       void Create(AddCustomerAndAdressContext model);
       void Update(AddCustomerAndAdressContext model, int id);

       

       void Save();
    }
}
