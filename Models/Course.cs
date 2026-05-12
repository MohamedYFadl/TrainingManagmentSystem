using System.ComponentModel.DataAnnotations.Schema;

namespace MVC02.Models
{
    public class Course:BaseClass
    {
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int Hours { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department Department { get; set; }
    }
}