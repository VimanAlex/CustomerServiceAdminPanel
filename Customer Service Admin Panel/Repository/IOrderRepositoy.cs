using Customer_Service_Admin_Panel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Repository
{
    internal interface IOrderRepositoy
    {
        IEnumerable<AddOrder> GetAllOrders(int id);
        AddOrder GetOrder(int id, int orderId);
        void CreateOrder(AddOrder order , int id);
        void UpdateOrder(int id,AddOrder model);
        void DeleteOrder(int id,int orderId);
        void Save();
    }
}
