using Customer_Service_Admin_Panel.Model;
using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Extension_Methods.Employees_Extensions
{
    public static class AddOrdersExtension
    {
        public static Order OrderModel(this AddOrder orderModel,Customer customer , Order order = null)
        {
            if(order == null)
            {
                order = new Order();
            }

            order.OrderTypeId = orderModel.OrderTypeId;
            order.CreateDate = orderModel.CreateDate;
            order.OrderStatusId = orderModel.OrderStatusId;

            return order;
        }

        public static OrderLine OrderLineModel(this AddOrder orderModel,Order order,Employee employee,OrderLine orderLine = null)
        {
            if(orderLine == null)
            {
                orderLine = new OrderLine();
            }

            orderLine.Order = order;
            orderLine.EmployeeId = employee.EmployeeId;
            orderLine.Amount = orderModel.Amount;

            return orderLine;
        }
    }
}