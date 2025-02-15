using Employee_Management_System.Interfaces;
using Employee_Management_System.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Employee_Management_System
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IVacationRepository, VacationRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
        }
    }
}
