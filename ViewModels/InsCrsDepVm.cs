using MVC02.Models;

namespace MVC02.ViewModels
{
    public class InsCrsDepVm
    {
        public int Id { get; set; }
        public string InsName { get; set; }
        public string InsAddress { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public decimal InsSalary { get; set; }
        public int DeptId { get; set; }
        public int CrsId { get; set; }
        public IReadOnlyList<Course> Courses { get; set; } = new List<Course>();
        public IReadOnlyList<Department> Departments { get; set; } = new List<Department>();


    }
}
