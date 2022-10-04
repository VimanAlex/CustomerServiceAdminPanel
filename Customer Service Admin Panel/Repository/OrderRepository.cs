using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.Extension_Methods.Employees_Extensions;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Repository
{
    public class OrderRepository : IOrderRepositoy
    {
        private CustomerServiceEntity dbEntity;

        public OrderRepository(CustomerServiceEntity context)
        {
            this.dbEntity = context;
        }

        public void CreateOrder(AddOrder model, int id)
        {
            Customer customer = dbEntity.Customers.Find(id);

            if (customer != null)
            {
                if (model.ListOfEmployees.Where(x => x.IsChecked).Count() != 0)
                {
                    var order = model.OrderModel(customer);
                    dbEntity.Orders.Add(order);

                    foreach (var emp in model.ListOfEmployees.Where(x => x.IsChecked))
                    {
                        var addOrdeLine = model.OrderLineModel(order, emp);
                        order.OrderLines.Add(addOrdeLine);

                    }
 
                }
                
            }
                
        }

        public IEnumerable<AddOrder> GetAllOrders(int id)
        {
            var dataListOrder = (from emp in dbEntity.Employees
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
                                         OrderType1 = ot.OrderType1,
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
                                         OrderType1 = x.OrderType1,
                                         OrderStatusId = x.OrderStatusId,
                                         OrderStatus = x.OrderStatus,
                                         OrderId = x.OrderId,
                                         OrderLineId = x.OrderLineId,
                                         CreateDate = x.CreateDate,
                                         Amount = x.Amount,
                                         FirstName = x.FirstName,
                                         LastName = x.LastName

                                     }).ToList();

            return dataListOrder;

        }

        public AddOrder GetOrder(int id ,int orderId)
        {
            var dataListOrder = (from emp in dbEntity.Employees
                                 join ol in dbEntity.OrderLines on emp.EmployeeId equals ol.EmployeeId
                                 join o in dbEntity.Orders on ol.OrderId equals o.OrderId
                                 join ot in dbEntity.OrderTypes on o.OrderTypeId equals ot.OrderTypeId
                                 join cust in dbEntity.Customers on emp.CustomerId equals cust.CustomerId
                                 join os in dbEntity.OrderStatus on o.OrderStatusId equals os.OrderStatusId

                                 where cust.CustomerId == id && o.OrderId == orderId
                                 select new
                                 {
                                     EmployeeId = emp.EmployeeId,
                                     CustomerId = cust.CustomerId,
                                     OrderTypeId = ot.OrderTypeId,
                                     OrderType1 = ot.OrderType1,
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
                                     OrderType1 = x.OrderType1,
                                     OrderStatusId = x.OrderStatusId,
                                     OrderStatus = x.OrderStatus,
                                     OrderId = x.OrderId,
                                     OrderLineId = x.OrderLineId,
                                     CreateDate = x.CreateDate,
                                     Amount = x.Amount,
                                     FirstName = x.FirstName,
                                     LastName = x.LastName

                                 }).FirstOrDefault();

            return dataListOrder;

        }

        public void Save()
        {
            dbEntity.SaveChanges();
        }

        public void UpdateOrder(int id, AddOrder model)
        {
            var order = dbEntity.Orders.Where(x => x.OrderId == model.OrderId).FirstOrDefault();
            var orderLine = dbEntity.OrderLines.Where(x => x.OrderId == model.OrderId).ToList();
            var customer = dbEntity.Customers.Find(id);

            var updateOrder = model.OrderModel(customer, order);
                      
            dbEntity.Entry(updateOrder).State = EntityState.Modified;

            foreach (var item in orderLine)
            {
                item.Amount = model.Amount;

                dbEntity.Entry(item).State = EntityState.Modified;
                
            }

        }

        public void DeleteOrder(int id, int orderId)
        {
            var customer = dbEntity.Customers.Find(id);

            if (customer != null)
            {
                var order = dbEntity.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
                var orderLine = dbEntity.OrderLines.Where(x => x.OrderId == order.OrderId);

                dbEntity.OrderLines.RemoveRange(orderLine);
                dbEntity.Orders.Remove(order);

            }

        }
    }
}