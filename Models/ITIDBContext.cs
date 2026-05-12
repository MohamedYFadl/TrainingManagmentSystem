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
                new Department { Id = 3, Name = "HR", Manager = "Samir" },
                new Department { Id = 4, Name = "Cyber Security", Manager = "Khaled" },
                new Department { Id = 5, Name = "AI", Manager = "Youssef" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "C#", Degree = 100, MinDegree = 50, Hours = 30, DeptId = 1 },
                new Course { Id = 2, Name = "SQL", Degree = 100, MinDegree = 50, Hours = 25, DeptId = 1 },
                new Course { Id = 3, Name = "CCNA", Degree = 100, MinDegree = 60, Hours = 40, DeptId = 2 },
                new Course { Id = 4, Name = "Recruitment", Degree = 100, MinDegree = 50, Hours = 20, DeptId = 3 },
                new Course { Id = 5, Name = "Ethical Hacking", Degree = 100, MinDegree = 65, Hours = 45, DeptId = 4 },
                new Course { Id = 6, Name = "Machine Learning", Degree = 100, MinDegree = 70, Hours = 50, DeptId = 5 }
            );

            modelBuilder.Entity<Trainee>().HasData(
                new Trainee { Id = 1, Name = "Ahmed", Address = "Cairo", Grade = 3, DeptId = 1, ImageUrl = "/Images/1.jpg" },
                new Trainee { Id = 2, Name = "Sara", Address = "Giza", Grade = 2, DeptId = 2, ImageUrl = "/Images/1.jpg" },
                new Trainee { Id = 3, Name = "Omar", Address = "Alex", Grade = 4, DeptId = 1, ImageUrl = "/Images/1.jpg" },
                new Trainee { Id = 4, Name = "Mariam", Address = "Mansoura", Grade = 1, DeptId = 3, ImageUrl = "/Images/1.jpg" },
                new Trainee { Id = 5, Name = "Yassin", Address = "Tanta", Grade = 2, DeptId = 4, ImageUrl = "/Images/1.jpg" },
                new Trainee { Id = 6, Name = "Salma", Address = "Cairo", Grade = 3, DeptId = 5, ImageUrl = "/Images/1.jpg" },
                new Trainee { Id = 7, Name = "Karim", Address = "Giza", Grade = 4, DeptId = 2, ImageUrl = "/Images/1.jpg" },
                new Trainee { Id = 8, Name = "Nada", Address = "Alex", Grade = 1, DeptId = 5, ImageUrl = "/Images/1.jpg" }
            );

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, Name = "Hassan", Salary = 8000, Address = "Cairo", DeptId = 1, Crs_id = 1, ImageUrl = "/Images/1.jpg" },
                new Instructor { Id = 2, Name = "Nour", Salary = 9000, Address = "Giza", DeptId = 2, Crs_id = 2, ImageUrl = "/Images/1.jpg" },
                new Instructor { Id = 3, Name = "Noura", Salary = 10000, Address = "Alex", DeptId = 2, Crs_id = 3, ImageUrl = "/Images/1.jpg" },
                new Instructor { Id = 4, Name = "Mahmoud", Salary = 11000, Address = "Cairo", DeptId = 3, Crs_id = 4, ImageUrl = "/Images/1.jpg" },
                new Instructor { Id = 5, Name = "Yara", Salary = 12000, Address = "Tanta", DeptId = 4, Crs_id = 5, ImageUrl = "/Images/1.jpg" },
                new Instructor { Id = 6, Name = "Mostafa", Salary = 15000, Address = "Giza", DeptId = 5, Crs_id = 6, ImageUrl = "/Images/1.jpg" }
            );

            modelBuilder.Entity<CrsResult>().HasData(
                new CrsResult { Id = 1, Degree = 40, Crs_Id = 1, TraineeId = 1 },
                new CrsResult { Id = 2, Degree = 90, Crs_Id = 2, TraineeId = 2 },
                new CrsResult { Id = 3, Degree = 75, Crs_Id = 1, TraineeId = 3 },
                new CrsResult { Id = 4, Degree = 30, Crs_Id = 3, TraineeId = 7 },
                new CrsResult { Id = 5, Degree = 88, Crs_Id = 4, TraineeId = 4 },
                new CrsResult { Id = 6, Degree = 95, Crs_Id = 5, TraineeId = 5 },
                new CrsResult { Id = 7, Degree = 70, Crs_Id = 6, TraineeId = 6 },
                new CrsResult { Id = 8, Degree = 98, Crs_Id = 6, TraineeId = 8 }
            );
        }

    }
}
