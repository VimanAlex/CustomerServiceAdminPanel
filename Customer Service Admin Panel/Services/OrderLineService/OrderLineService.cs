using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Services.OrderLineService
{
    public class OrderLineService : IOrderLineService
    {
        private UnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;

        public OrderLineService(UnitOfWork unitOfWork, ModelStateDictionary modelState)
        {
            _unitOfWork = unitOfWork;
            _modelState = modelState;
        }

        public IEnumerable<AddOrder> GetAllOrderLines(int id, int orderId)
        {
           var orderLines = _unitOfWork.OrderLineRepository.GetAllOrderLines(id,orderId);
           return orderLines;
        }

        public List<AddOrder> GetSelectListOrderLine(int id, int orderId)
        {
           var getAllOrderLineSelectList = _unitOfWork.OrderLineRepository.GetAllOrderLines(id, orderId).ToList();
            return getAllOrderLineSelectList;
        }
    }
}