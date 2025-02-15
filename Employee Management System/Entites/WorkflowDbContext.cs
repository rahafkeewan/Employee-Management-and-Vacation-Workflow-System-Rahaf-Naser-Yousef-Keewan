using Employee_Management_System.Entites;
using Microsoft.EntityFrameworkCore;

public class WorkflowDbContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<VacationType> VacationTypes { get; set; }
    public DbSet<RequestState> RequestStates { get; set; }
    public DbSet<VacationRequest> VacationRequests { get; set; }

    public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=YOUSEF;Database=EmployeeSystemDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Position)
            .WithMany()
            .HasForeignKey(e => e.PositionId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.ReportedToEmployee)
            .WithMany()
            .HasForeignKey(e => e.ReportedToEmployeeNumber)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.Employee)
            .WithMany()
            .HasForeignKey(vr => vr.EmployeeNumber);

        modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.VacationType)
            .WithMany()
            .HasForeignKey(vr => vr.VacationTypeCode);

        modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.RequestState)
            .WithMany()
            .HasForeignKey(vr => vr.RequestStateId);

        modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.ApprovedByEmployee)
            .WithMany()
            .HasForeignKey(vr => vr.ApprovedByEmployeeNumber)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.DeclinedByEmployee)
            .WithMany()
            .HasForeignKey(vr => vr.DeclinedByEmployeeNumber)
            .OnDelete(DeleteBehavior.Restrict);
    }
}