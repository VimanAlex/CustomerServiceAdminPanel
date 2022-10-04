using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Repository
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private CustomerServiceEntity dbEntity;
        
        public OrderStatusRepository(CustomerServiceEntity context)
        {
            this.dbEntity = context;
        }

        public IEnumerable<OrderStatu> GetAllOrderStatus()
        {
            var data = dbEntity.OrderStatus.ToList();
            return data;
        }
    }
}