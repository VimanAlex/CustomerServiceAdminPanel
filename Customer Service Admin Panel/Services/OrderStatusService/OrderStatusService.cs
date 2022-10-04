using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Services.OrderStatusService
{
    public class OrderStatusService : IOrderStatusService
    {
        private UnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;

        public OrderStatusService(UnitOfWork unitOfWork, ModelStateDictionary modelState)
        {
            _unitOfWork = unitOfWork;
            _modelState = modelState;
        }

        public IEnumerable<OrderStatu> GetAllOrderStatus()
        {
            var orderStatus = _unitOfWork.OrderStatusRepository.GetAllOrderStatus();
            return orderStatus;
        }

        public List<OrderStatu> GetSelectListOrderStatus()
        {
            var getSelectListOrdeStatus = _unitOfWork.OrderStatusRepository.GetAllOrderStatus().ToList();
            return getSelectListOrdeStatus;
        }
    }
}