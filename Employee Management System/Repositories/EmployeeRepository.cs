using Employee_Management_System.Entites;
using Employee_Management_System.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly WorkflowDbContext _context;

        public EmployeeRepository(WorkflowDbContext context)
        {
            _context = context;
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            var emp = _context.Employees.FirstOrDefault(x => x.EmployeeNumber == employee.EmployeeNumber);
            if (emp != null)
            {
                emp.EmployeeName = employee.EmployeeName;
                emp.Salary = employee.Salary;
                emp.DepartmentId = employee.DepartmentId;
                emp.PositionId = employee.PositionId;
                emp.GenderCode = employee.GenderCode;
                emp.Salary = employee.Salary;
            }
            _context.SaveChanges();
        }

        public Employee? GetEmployee(string employeeNumber)
        {
            if (employeeNumber == "") { return null; }
            return _context.Employees
                .Include(x => x.Department)
                .Include(x => x.Position)
                .FirstOrDefault(x => x.EmployeeNumber == employeeNumber);
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees
                .Include(x => x.Department)
                .Include(x => x.Position)
                .ToList();
        }
        public List<Employee> GetEmployeesWithPendingRequests()
        {
            var empNums = _context.VacationRequests.Where(vr => vr.RequestStateId == 1).Select(x => x.EmployeeNumber).ToList();
            return _context.Employees
                .Include(x => x.Department)
                .Include(x => x.Position)
                .ToList()
                .Where(x => empNums.Contains(x.EmployeeNumber))
                .ToList();
        }
        public bool UpdateVacationDaysBalance(string EmployeeNumber, int TotalVacationDays)
        {
            var EMP = GetEmployee(EmployeeNumber);
            if (EMP == null) { return false; }
            if (TotalVacationDays < EMP.VacationDaysLeft)
            {
                EMP.VacationDaysLeft -= TotalVacationDays;
            }
            else
            {
                EMP.VacationDaysLeft = 0;
            }
            _context.SaveChanges();
            return true;
        }
    }
}
