using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Services.OrderStatusService
{
    public interface IOrderStatusService
    {
        IEnumerable<OrderStatu> GetAllOrderStatus();
        List<OrderStatu> GetSelectListOrderStatus();

    }
}
