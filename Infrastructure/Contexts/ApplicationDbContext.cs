using Microsoft.EntityFrameworkCore;
using Domain.Entities;  
namespace Infrastructure.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Checkin> Checkins { get; set; }

    public virtual DbSet<DailyReport> DailyReports { get; set; }

    public virtual DbSet<DailyReportCompletedTask> DailyReportCompletedTasks { get; set; }

    public virtual DbSet<DailyReportPlannedTask> DailyReportPlannedTasks { get; set; }

    public virtual DbSet<DailyWorkSummary> DailyWorkSummaries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesList> EmployeesLists { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<WorkPlan> WorkPlans { get; set; }

    public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Checkin>(entity =>
        {
            entity.HasKey(e => e.CheckinId).HasName("PK__Checkins__F3C85D51E292FB00");

            entity.HasIndex(e => e.EmployeeId, "IX_Checkins_EmployeeID");

            entity.Property(e => e.CheckinId).HasColumnName("CheckinID");
            entity.Property(e => e.CheckinTime)
            .HasColumnType("timestamp without time zone");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Notes).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.Checkins)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Checkins_Employees");
        });

        modelBuilder.Entity<DailyReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__DailyRep__D5BD48E5FDBC9777");

            entity.HasIndex(e => new { e.EmployeeId, e.ReportDate }, "UQ_DailyReports_Employee_Date").IsUnique();

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FinalizedTime).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ReportDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.TotalWorkHours).HasPrecision(5, 2);
            entity.Property(e => e.WorkStatus).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.DailyReports)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyReports_Employees");
        });

        modelBuilder.Entity<DailyReportCompletedTask>(entity =>
        {
            entity.HasKey(e => e.ReportCompletedTaskId).HasName("PK__DailyRep__77D00B9D3609CD06");

            entity.ToTable("DailyReport_CompletedTasks");

            entity.HasIndex(e => e.TaskId, "IX_DailyReport_CompletedTasks_TaskID");

            entity.HasIndex(e => new { e.ReportId, e.TaskId }, "UQ_ReportCompletedTasks_Report_Task").IsUnique();

            entity.Property(e => e.ReportCompletedTaskId).HasColumnName("ReportCompletedTaskID");
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.Report).WithMany(p => p.DailyReportCompletedTasks)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportCompletedTasks_Report");

            entity.HasOne(d => d.Task).WithMany(p => p.DailyReportCompletedTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportCompletedTasks_EmployeesList");
        });

        modelBuilder.Entity<DailyReportPlannedTask>(entity =>
        {
            entity.HasKey(e => e.ReportPlannedTaskId).HasName("PK__DailyRep__BF13C8980D7F5F7A");

            entity.ToTable("DailyReport_PlannedTasks");

            entity.HasIndex(e => e.TaskId, "IX_DailyReport_PlannedTasks_TaskID");

            entity.HasIndex(e => new { e.ReportId, e.TaskId }, "UQ_ReportPlannedTasks_Report_Task").IsUnique();

            entity.Property(e => e.ReportPlannedTaskId).HasColumnName("ReportPlannedTaskID");
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.Report).WithMany(p => p.DailyReportPlannedTasks)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportPlannedTasks_Report");

            entity.HasOne(d => d.Task).WithMany(p => p.DailyReportPlannedTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportPlannedTasks_EmployeesList");
        });

        modelBuilder.Entity<DailyWorkSummary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DailyWor__3214EC072063D57A");

            entity.ToTable("DailyWorkSummary");

            entity.HasIndex(e => e.EmployeeId, "IX_DailyWorkSummary_EmployeeId");
            entity.Property(e => e.WorkDate).HasColumnType("date");
            entity.Property(e => e.FirstCheckin).HasColumnType("timestamp without time zone");
            entity.Property(e => e.LastCheckout).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalCalculatedHours).HasPrecision(5, 2);

            entity.HasOne(d => d.Employee).WithMany(p => p.DailyWorkSummaries)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyWorkSummary_Employees");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1311F0C96");

            entity.HasIndex(e => e.ManagerId, "IX_Employees_ManagerID");

            entity.HasIndex(e => e.PositionId, "IX_Employees_PositionID");

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D1053453A29024").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmploymentStatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Active'::character varying");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.PositionId).HasColumnName("PositionID");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Employees_Manager");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Positions");
        });

        modelBuilder.Entity<EmployeesList>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D199CAF273");

            entity.ToTable("EmployeesList");

            entity.HasIndex(e => e.AssignedToEmployeeId, "IX_EmployeesList_AssignedToEmployeeID");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.AssignedToEmployeeId).HasColumnName("AssignedToEmployeeID");
            entity.Property(e => e.CompletionDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Normal'::character varying");
            entity.Property(e => e.RelatedProject).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Not Started'::character varying");
            entity.Property(e => e.TaskName).HasMaxLength(255);

            entity.HasOne(d => d.AssignedToEmployee).WithMany(p => p.EmployeesLists)
                .HasForeignKey(d => d.AssignedToEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeesList_Employees");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A59D6236541");

            entity.HasIndex(e => e.PositionName, "UQ__Position__E46AEF4299F9F60F").IsUnique();

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.PositionName).HasMaxLength(100);
        });

        modelBuilder.Entity<WorkPlan>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_WorkPlans_EmployeeId");

            entity.Property(e => e.PlannedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.TaskDescription).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkPlans)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_WorkPlan_Employee");
        });

        modelBuilder.Entity<WorkSchedule>(entity =>
        {
            entity.HasKey(e => e.WorkScheduleId).HasName("PK__WorkSche__C6AC635EC0F1DC14");

            entity.HasIndex(e => new { e.EmployeeId, e.ScheduleDate }, "UQ_WorkSchedules_Employee_Date").IsUnique();

            entity.Property(e => e.WorkScheduleId).HasColumnName("WorkScheduleID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.ScheduleType).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkSchedules)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkSchedules_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
