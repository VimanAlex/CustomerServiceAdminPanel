using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Repository
{
    internal interface IProductRepository
    {
        IEnumerable<Product> GetAllProduct();

        void AddProduct(Product product);
        void DeleteProduct(int id);
    }
}
