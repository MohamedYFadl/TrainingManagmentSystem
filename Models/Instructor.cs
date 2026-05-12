using System.ComponentModel.DataAnnotations.Schema;

namespace MVC02.Models
{
    public class Instructor:BaseClass
    {
        public string ImageUrl { get; set; }
        public decimal Salary { get; set; }
        public string? Address { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department Department { get; set; }
        [ForeignKey("Course")]
        public int Crs_id { get; set; }
        public Course Course { get; set; }
    }
}
