using Employee_Management_System.Entites;
using Employee_Management_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly WorkflowDbContext _context;

        public DepartmentRepository(WorkflowDbContext context)
        {
            _context = context;
        }

        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}
