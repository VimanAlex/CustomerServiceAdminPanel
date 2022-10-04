using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Repository
{
    public class ProductRepository : IProductRepository
    {
        private CustomerServiceEntity dbEntity;

        public ProductRepository(CustomerServiceEntity context)
        {
            this.dbEntity = context;
        }

        public void AddProduct(Product product)
        {

            dbEntity.Products.Add(product);
        }

        public void DeleteProduct(int id)
        {
            var product = dbEntity.Products.Find(id);
            dbEntity.Products.Remove(product);
        }

        public IEnumerable<Product> GetAllProduct()
        {
           var listProduct =  dbEntity.Products.ToList();

            return listProduct;
        }


    }
}