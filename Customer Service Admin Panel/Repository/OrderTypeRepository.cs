using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Repository
{
    public class OrderTypeRepository : IOrderTypeRepository
    {
        private CustomerServiceEntity dbEntity;
       
        public OrderTypeRepository(CustomerServiceEntity context)
        {
            this.dbEntity = context;
        }

        public IEnumerable<OrderType> GetAllOrderType()
        {
            var data = dbEntity.OrderTypes.ToList();
            return data;
        }
    }
}