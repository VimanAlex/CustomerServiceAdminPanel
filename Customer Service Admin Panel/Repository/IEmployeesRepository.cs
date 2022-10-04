using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Repository
{
    internal interface IEmployeesRepository
    {
        IEnumerable<Employee> GetAllEmployee(int id);
        void AddEmployee(Employee employee,int id);
        void DeleteEmployee(int employeeId,int id);

        void Save();
    }
}
