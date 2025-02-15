using Employee_Management_System.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Employee_Management_System.Interfaces
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        Employee? GetEmployee(string employeeNumber);
        List<Employee> GetAllEmployees();
        List<Employee> GetEmployeesWithPendingRequests();
        bool UpdateVacationDaysBalance(string EmployeeNumber, int TotalVacationDays);
    }
}
