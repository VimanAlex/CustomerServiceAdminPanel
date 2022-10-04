using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Repository
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private CustomerServiceEntity dbEntity;
      
        public OrderLineRepository(CustomerServiceEntity context)
        {
            this.dbEntity = context;
        }

        public IEnumerable<AddOrder> GetAllOrderLines(int id,int orderId)
        {
            var dataListOrderLine = (from emp in dbEntity.Employees
                                     join ol in dbEntity.OrderLines on emp.EmployeeId equals ol.EmployeeId
                                     join o in dbEntity.Orders on ol.OrderId equals o.OrderId
                                     join ot in dbEntity.OrderTypes on o.OrderTypeId equals ot.OrderTypeId
                                     join cust in dbEntity.Customers on emp.CustomerId equals cust.CustomerId
                                     join os in dbEntity.OrderStatus on o.OrderStatusId equals os.OrderStatusId

                                     where cust.CustomerId == id
                                     select new
                                     {
                                         EmployeeId = emp.EmployeeId,
                                         CustomerId = cust.CustomerId,
                                         OrderTypeId = ot.OrderTypeId,
                                         OrderStatusId = os.OrderStatusId,
                                         OrderStatus = os.OrderStatus,
                                         OrderId = o.OrderId,
                                         OrderLineId = ol.OrderLineId,
                                         CreateDate = o.CreateDate,
                                         Amount = ol.Amount,
                                         FirstName = emp.FirstName,
                                         LastName = emp.LastName
                                     }).AsEnumerable().Select(x => new AddOrder()
                                     {
                                         EmployeeId = x.EmployeeId,
                                         CustomerId = x.CustomerId,
                                         OrderTypeId = x.OrderTypeId,
                                         OrderStatusId = x.OrderStatusId,
                                         OrderStatus = x.OrderStatus,
                                         OrderId = x.OrderId,
                                         OrderLineId = x.OrderLineId,
                                         CreateDate = x.CreateDate,
                                         Amount = x.Amount,
                                         FirstName = x.FirstName,
                                         LastName = x.LastName

                                     }).Where(x=>x.OrderId == orderId).ToList();

            return dataListOrderLine;
        }
    }
}