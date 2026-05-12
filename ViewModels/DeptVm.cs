using MVC02.Models;

namespace MVC02.ViewModels
{
    public class DeptVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public IReadOnlyList<Course> Courses { get; set; }
        public IReadOnlyList<Instructor> Instructors { get; set; }
        public IReadOnlyList<Trainee> Trainees { get; set; }
    }
}
