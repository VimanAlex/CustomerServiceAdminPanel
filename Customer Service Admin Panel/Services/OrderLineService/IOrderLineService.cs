using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Services.OrderLineService
{
    public interface IOrderLineService
    {
        IEnumerable<AddOrder> GetAllOrderLines(int id, int orderId);
        List<AddOrder> GetSelectListOrderLine(int id, int orderId);

    }
}
