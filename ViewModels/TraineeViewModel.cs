using MVC02.Models;

namespace MVC02.ViewModels
{
    public class TraineeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public int DeptId { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public int Grade { get; set; }
        public ICollection<CrsResult> CrsResults { get; set; }

    }
}
