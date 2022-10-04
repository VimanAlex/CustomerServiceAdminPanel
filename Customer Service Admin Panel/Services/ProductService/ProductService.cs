using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Services.ProductService
{
    public class ProductService : IProductService
    {
        private UnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;

        public ProductService(UnitOfWork unitOfWork, ModelStateDictionary modelState)
        {
            _unitOfWork = unitOfWork;
            _modelState = modelState;
        }

        public void AddProduct(Product product)
        {
            _unitOfWork.ProductRepository.AddProduct(product);
            _unitOfWork.Save();
        }

        public void DeleteProduct(int id)
        {
            _unitOfWork.ProductRepository.DeleteProduct(id);
            _unitOfWork.Save();
        }

        public IEnumerable<Product> GetAllProduct()
        {
           var products = _unitOfWork.ProductRepository.GetAllProduct();

            return products;
        }
    }
}