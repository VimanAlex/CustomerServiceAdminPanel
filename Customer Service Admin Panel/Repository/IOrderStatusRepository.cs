using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Repository
{
    internal interface IOrderStatusRepository
    {
        IEnumerable<OrderStatu> GetAllOrderStatus();
    }
}
