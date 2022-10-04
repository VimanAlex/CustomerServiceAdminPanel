using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.Repository
{
    public interface IOrderTypeRepository
    {
        IEnumerable<OrderType> GetAllOrderType();
    }
}