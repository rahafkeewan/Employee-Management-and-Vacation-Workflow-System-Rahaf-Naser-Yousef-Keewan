using Employee_Management_System;
using Employee_Management_System.Entites;
using Employee_Management_System.Interfaces;
using Employee_Management_System.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<WorkflowDbContext>(options =>
            options.UseSqlServer("server=YOUSEF;database=SystemDB;TrustServerCertificate=True;Integrated Security=True"));
        services.AddRepositories();

        var serviceProvider = services.BuildServiceProvider();

        using (var context = serviceProvider.GetRequiredService<WorkflowDbContext>())
        {
            if (!context.Departments.Any())
            {
                for (int i = 1; i <= 20; i++)
                {
                    context.Departments.Add(new Department { DepartmentName = "Department " + i });
                }
                context.SaveChanges();
            }

            if (!context.Positions.Any())
            {
                for (int i = 1; i <= 20; i++)
                {
                    context.Positions.Add(new Position { PositionName = "Position " + i });
                }
                context.SaveChanges();
            }
            var ListOfDepartment = context.Departments.ToList();
            var ListOfPositions = context.Positions.ToList();

            if (!context.Employees.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    context.Employees.Add(new Employee()
                    {
                        EmployeeNumber = i.ToString(),
                        EmployeeName = "Employee " + i,
                        DepartmentId = ListOfDepartment.FirstOrDefault()?.DepartmentId ?? 0,
                        PositionId = ListOfPositions.FirstOrDefault()?.PositionId ?? 0,
                        GenderCode = "F",
                        Salary = 4000.00m
                    });
                }
                context.SaveChanges();
            }
            if (!context.RequestStates.Any())
            {
                context.RequestStates.Add(new RequestState { StateName = "Submitted" });
                context.RequestStates.Add(new RequestState { StateName = "Approved  " });
                context.RequestStates.Add(new RequestState { StateName = "Declined" });
                context.SaveChanges();
            }
            if (!context.VacationTypes.Any())
            {
                context.VacationTypes.Add(new VacationType { VacationTypeCode = "S", VacationTypeName = "Sick" });
                context.VacationTypes.Add(new VacationType { VacationTypeCode = "U", VacationTypeName = "Unpaid" });
                context.VacationTypes.Add(new VacationType { VacationTypeCode = "A", VacationTypeName = "Annual" });
                context.VacationTypes.Add(new VacationType { VacationTypeCode = "O", VacationTypeName = "Day Of" });
                context.VacationTypes.Add(new VacationType { VacationTypeCode = "B", VacationTypeName = "Business Trip" });
                context.SaveChanges();
            }
            while (true)
            {
                Console.WriteLine("\nWelcome to Employee Management and Vacation Workflow System!");
                Console.WriteLine("1. View Employee Information");
                Console.WriteLine("2. Update Employee Information");
                Console.WriteLine("3. Submit Vacation Request");
                Console.WriteLine("4. Show Pending Requests");
                Console.WriteLine("5. Approve Vacation Request");
                Console.WriteLine("6. Decline Vacation Request");
                Console.WriteLine("7. Show All Employees");
                Console.WriteLine("8. Show Employees With Pending Requests");
                Console.WriteLine("9. Show Approved Vacation History");
                Console.WriteLine("10. Exit");

                Console.Write("\nEnter your choice: ");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShowEmployeeInfo(context);
                            break;
                        case 2:
                            UpdateEmployee(context);
                            break;
                        case 3:
                            SubmitVacationRequest(context);
                            break;
                        case 4:
                            ShowPendingRequests(context);
                            break;
                        case 5:
                            ApproveVacationRequest(context);
                            return;
                        case 6:
                            DeclineVacationRequest(context);
                            return;
                        case 7:
                            GetAllEmployees(context);
                            return;
                        case 8:
                            GetEmployeesWithPendingRequests(context);
                            return;
                        case 9:
                            GetApprovedVacationHistory(context);
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

    }
    static void ShowEmployeeInfo(WorkflowDbContext context)
    {
        var employeeRepository = new EmployeeRepository(context);
        Console.Write("\nEnter your Employee Number: ");
        var EMPNumber = Console.ReadLine() ?? "";
        while (EMPNumber == "")
        {
            Console.Write("\nEntered Employee Number is Invalid");
            Console.Write("\nEnter your Employee Number: ");
            EMPNumber = Console.ReadLine() ?? "";
        }
        var EMP = employeeRepository.GetEmployee(EMPNumber);
        if (EMP == null)
        {
            Console.Write("\nEntered Employee Number is Invalid");
            Console.Write("\nEnter your Employee Number: ");
            EMPNumber = Console.ReadLine() ?? "";
        }
        Console.Write($"\nEmployee Number is: {EMP?.EmployeeNumber}");
        Console.Write($"\nEmployee Name is: {EMP?.EmployeeName}");
        Console.Write($"\nEmployee Department is: {EMP?.Department.DepartmentName}");
        Console.Write($"\nEmployee Position is: {EMP?.Position.PositionName}");
        Console.Write($"\nEmployee Vacation Days Left is: {EMP?.VacationDaysLeft}");
        Console.Write($"\nReported To Employee Name is: {EMP?.ReportedToEmployee?.EmployeeName}");
        Console.WriteLine("\n");
    }
    static void UpdateEmployee(WorkflowDbContext context)
    {
        var employeeRepository = new EmployeeRepository(context);
        Console.Write("Enter employee number to update employee data: ");
        string empNumber = Console.ReadLine() ?? "";

        Console.Write("Enter new name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter the new section number: ");
        int deptId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter new job number: ");
        int posId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter new salary: ");
        decimal salary = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter new GenderCode (M,F): ");
        string GenderCode = Console.ReadLine() ?? "";

        var Emp = new Employee() { EmployeeNumber = empNumber, EmployeeName = name, DepartmentId = deptId, PositionId = posId, Salary = salary, GenderCode = GenderCode };
        employeeRepository.UpdateEmployee(Emp);
        Console.WriteLine("Employee data has been updated successfully.");
    }
    static void SubmitVacationRequest(WorkflowDbContext context)
    {
        var vacationRepository = new VacationRepository(context);
        Console.Write("Enter employee number: ");
        string empNumber = Console.ReadLine() ?? "";

        Console.Write("Enter vacation type (S, U, A, O, B): ");
        string vacType = Console.ReadLine() ?? "";

        Console.Write("Enter start date (yyyy-MM-dd): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine() ?? "0001-01-01");

        Console.Write("Enter end date (yyyy-MM-dd): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine() ?? "0001-01-01");

        int totalDays = (endDate - startDate).Days + 1;

        var request = new VacationRequest
        {
            EmployeeNumber = empNumber,
            VacationTypeCode = vacType,
            StartDate = startDate,
            EndDate = endDate,
            TotalVacationDays = totalDays,
            RequestStateId = 1,
            Description = "New vacation request"
        };

        vacationRepository.SubmitVacationRequest(request);
        Console.WriteLine("Application submitted successfully.");
    }
    static void ShowPendingRequests(WorkflowDbContext context)
    {
        var vacationRepository = new VacationRepository(context);
        Console.Write("Enter employee number to view pending requests: ");
        string empNumber = Console.ReadLine() ?? "";

        var requests = vacationRepository.GetPendingRequests(empNumber);

        if (!requests.Any())
        {
            Console.WriteLine("There are no pending requests..");
            return;
        }

        foreach (var req in requests)
        {
            Console.WriteLine($"Request number {req.RequestId} - Employee: {req.EmployeeNumber} - From {req.StartDate.ToShortDateString()} To {req.EndDate.ToShortDateString()}.");
        }
    }
    static void ApproveVacationRequest(WorkflowDbContext context)
    {
        var vacationRepository = new VacationRepository(context);
        Console.Write("Enter the order number to approve it: ");
        int requestId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter the corresponding employee number: ");
        string approver = Console.ReadLine() ?? "0";

        vacationRepository.ApproveVacationRequest(requestId, approver);
        Console.WriteLine("The request has been successfully approved.");
    }
    static void DeclineVacationRequest(WorkflowDbContext context)
    {
        var vacationRepository = new VacationRepository(context);
        Console.Write("Enter the order number to reject it: ");
        int requestId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter the employee's number: ");
        string decliner = Console.ReadLine() ?? "";

        vacationRepository.DeclineVacationRequest(requestId, decliner);
        Console.WriteLine("The request was successfully rejected.");
    }
    static void GetAllEmployees(WorkflowDbContext context)
    {
        var employeeRepository = new EmployeeRepository(context);
        var employees = employeeRepository.GetAllEmployees();

        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.EmployeeNumber} - {emp.EmployeeName} - {emp.VacationDaysLeft} - {emp.Salary} - {emp.Department.DepartmentName}");
        }
    }
    static void GetEmployeesWithPendingRequests(WorkflowDbContext context)
    {
        var employeeRepository = new EmployeeRepository(context);

        var employees = employeeRepository.GetEmployeesWithPendingRequests();

        Console.WriteLine("Employees with pending requests:");
        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.EmployeeNumber} - {emp.EmployeeName} - {emp.VacationDaysLeft} - {emp.Salary} - {emp.Department.DepartmentName}");
        }
    }
    static void GetApprovedVacationHistory(WorkflowDbContext context)
    {
        var vacationRepository = new VacationRepository(context);
        Console.Write("Enter employee number: ");
        string empNumber = Console.ReadLine() ?? "";

        var history = vacationRepository.GetApprovedVacationHistory(empNumber);

        foreach (var entry in history)
        {
            Console.WriteLine($"Vacation type: {entry.VacationType.VacationTypeName} - {entry.StartDate} - {entry.TotalVacationDays} - {entry.Employee.EmployeeName}");
        }
    }
}
