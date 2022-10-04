using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.ModelView;
using Customer_Service_Admin_Panel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private UnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;
        private OrderValidation OrderValidation = new OrderValidation();

        public OrderService(UnitOfWork unitOfWork, ModelStateDictionary modelState)
        {
            _unitOfWork = unitOfWork;
            _modelState = modelState;
        }

       

        public void CreateOrder(AddOrder order, int id)
        {
            if (OrderValidation.IsValid(order,_modelState))
            {
                _unitOfWork.OrderRepository.CreateOrder(order, id);
                _unitOfWork.Save();
            }

            
        }

        public void DeleteOrder(int id, int orderId)
        {
            _unitOfWork.OrderRepository.DeleteOrder(id, orderId);
            _unitOfWork.Save();
        }

        public IEnumerable<AddOrder> GetAllOrders(int id)
        {
            var orders = _unitOfWork.OrderRepository.GetAllOrders(id);

            return orders;
        }

        public AddOrder GetOrder(int id, int orderId)
        {
            var getOrder = _unitOfWork.OrderRepository.GetOrder(id,orderId);
            return getOrder;
        }

        public void UpdateOrder(int id, AddOrder model)
        {
            _unitOfWork.OrderRepository.UpdateOrder(id, model);
            _unitOfWork.Save();
        }
    }
}