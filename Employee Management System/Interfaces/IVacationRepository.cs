using Employee_Management_System.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Interfaces
{
    public interface IVacationRepository
    {
        void SubmitVacationRequest(VacationRequest request);
        void ApproveVacationRequest(int requestId, string approvedByEmployeeNumber);
        void DeclineVacationRequest(int requestId, string declinedByEmployeeNumber);
        IEnumerable<VacationRequest> GetPendingRequests(string employeeNumber);
        List<VacationRequest> GetApprovedVacationHistory(string employeeNumber);
    }
}
