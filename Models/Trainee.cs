using System.ComponentModel.DataAnnotations.Schema;

namespace MVC02.Models
{
    public class Trainee:BaseClass
    {
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public int Grade { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department Department { get; set; }
    }
}
