using Customer_Service_Admin_Panel.DAL;
using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.Services.OrderLineService
{
    public class OrderTypeService : IOrderTypeSerice
    {
        private UnitOfWork _unitOfWork;
        private ModelStateDictionary _modelStatus;

        public OrderTypeService(UnitOfWork unitOfWork, ModelStateDictionary modelStatus)
        {
            _unitOfWork = unitOfWork;
            _modelStatus = modelStatus;
        }

        public IEnumerable<OrderType> GetAllOrderType()
        {
            var orderType = _unitOfWork.OrderTypeRepository.GetAllOrderType();
            return orderType;
        }

        public List<OrderType> GetSelectListOrderType()
        {
            var selectListOrderType = _unitOfWork.OrderTypeRepository.GetAllOrderType().ToList();

            return selectListOrderType;
        }
    }
}