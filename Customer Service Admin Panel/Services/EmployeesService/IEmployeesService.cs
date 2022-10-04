using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Service_Admin_Panel.Services.EmployeesService
{
    public interface IEmployeesService
    {
        IEnumerable<Employee> GetAllEmployee(int id);
        void AddEmployee(Employee employee, int id);
        void DeleteEmployee(int employeeId, int id);
    }
}
