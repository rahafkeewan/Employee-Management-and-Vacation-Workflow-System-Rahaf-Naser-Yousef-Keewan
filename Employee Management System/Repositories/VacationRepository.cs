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
    public class VacationRepository : IVacationRepository
    {
        private readonly WorkflowDbContext _context;
        private readonly EmployeeRepository _employeeRepository;

        public VacationRepository(WorkflowDbContext context)
        {
            _context = context;
            _employeeRepository = new EmployeeRepository(context);
        }
        public IEnumerable<VacationRequest> GetPendingRequests(string employeeNumber)
        {
            return _context.VacationRequests
                .Where(vr => vr.EmployeeNumber == employeeNumber && vr.RequestStateId == 1)
                .ToList();
        }
        public void SubmitVacationRequest(VacationRequest request)
        {
            if (!_context.VacationRequests.Any(vr => vr.EmployeeNumber == request.EmployeeNumber &&
                vr.StartDate <= request.EndDate && vr.EndDate >= request.StartDate))
            {
                _context.VacationRequests.Add(request);
                _context.SaveChanges();
            }
        }

        public void ApproveVacationRequest(int requestId, string approverEmployeeNumber)
        {
            var request = _context.VacationRequests.FirstOrDefault(vr => vr.RequestId == requestId);
            if (request != null)
            {
                request.RequestStateId = 2;
                request.ApprovedByEmployeeNumber = approverEmployeeNumber;
                _employeeRepository.UpdateVacationDaysBalance(request.EmployeeNumber, request.TotalVacationDays);
                _context.SaveChanges();
            }
        }

        public void DeclineVacationRequest(int requestId, string declinedByEmployeeNumber)
        {
            var request = _context.VacationRequests.FirstOrDefault(vr => vr.RequestId == requestId);
            if (request != null)
            {
                request.RequestStateId = 3;
                request.DeclinedByEmployeeNumber = declinedByEmployeeNumber;
                _context.SaveChanges();
            }
        }
        public List<VacationRequest> GetApprovedVacationHistory(string employeeNumber)
        {
            var history = _context.VacationRequests
            .Where(vr => vr.EmployeeNumber == employeeNumber && vr.RequestStateId == 2)
            .Include(x => x.VacationType)
            .Include(x => x.Employee)
            .ToList();
            return history;

        }
    }
}
