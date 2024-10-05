using Microsoft.EntityFrameworkCore;
using School.Data.Entities;

namespace School.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmetSubject> DepartmetSubjects { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<InstructorSubject> InstructorSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<InstructorSubject>()
            .HasKey(e => new { e.SubID, e.InstId });

        modelBuilder.Entity<DepartmetSubject>()
            .HasKey(e => new { e.SubID, e.DID });

        modelBuilder.Entity<StudentSubject>()
            .HasKey(e => new { e.SubID, e.StudID });

    }
}
