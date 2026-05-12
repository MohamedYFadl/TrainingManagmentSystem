using Microsoft.EntityFrameworkCore;

namespace MVC02.Models
{
    public class ITIDBContext : DbContext
    {
        public ITIDBContext(DbContextOptions<ITIDBContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<CrsResult> CrsResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Trainee>()
                .HasOne(i => i.Department)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(i => i.Department)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Course)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CrsResult>()
                .HasOne(i => i.Course)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CrsResult>()
                .HasOne(i => i.Trainee)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Department>().HasData(
       new Department { Id = 1, Name = "SD", Manager = "Ali" },
            new Department { Id = 2, Name = "Network", Manager = "Mona" },
             new Department { Id = 3, Name = "HR", Manager = "Samir" });

            modelBuilder.Entity<Course>().HasData(
        new Course { Id = 1, Name = "C#", Degree = 100, MinDegree = 50, Hours = 30, DeptId = 1 },
             new Course { Id = 2, Name = "SQL", Degree = 100, MinDegree = 50, Hours = 25, DeptId = 1 });

            modelBuilder.Entity<Trainee>().HasData(
       new Trainee { Id = 1, Name = "Ahmed", Address = "Cairo", Grade = 3, DeptId = 1, ImageUrl = "/Images/1.jpg" },
            new Trainee { Id = 2, Name = "Sara", Address = "Giza", Grade = 2, DeptId = 2, ImageUrl = "/Images/1.jpg" });
            modelBuilder.Entity<Instructor>().HasData(
       new Instructor { Id = 1, Name = "Hassan", Salary = 8000, Address = "Cairo", DeptId = 1, Crs_id = 1, ImageUrl = "/Images/1.jpg" },
            new Instructor { Id = 2, Name = "Nour", Salary = 9000, Address = "Giza", DeptId = 2, Crs_id = 2, ImageUrl = "/Images/1.jpg" },
            new Instructor { Id = 3, Name = "Noura", Salary = 10000, Address = "Alex", DeptId = 2, Crs_id = 2, ImageUrl = "/Images/1.jpg" });

            modelBuilder.Entity<CrsResult>().HasData(
        new CrsResult { Id = 1, Degree = 85, Crs_Id = 1, TraineeId = 1 },
             new CrsResult { Id = 2, Degree = 90, Crs_Id = 2, TraineeId = 2 });
        }

    }
}
