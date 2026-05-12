using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC02.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC02.ViewModels
{
    public class CourseDeptVm
    {
        public int Id { get; set; }
        [MinLength(2,ErrorMessage = "Name must be at least 2 charchaters")]
        [UniquePerDept]
        public string Name { get; set; }
        [Range(50,100,ErrorMessage = "Degree must between 50-100")]
        public int Degree { get; set; }
        [Remote("LessThanDegree","Course",AdditionalFields = "Degree")]//ErrorMessage = "Min Degree must be less than degree"
        public int MinDegree { get; set; }
        [DividedByThree]
        public int Hours { get; set; } 
        public int DeptId { get; set; }
        public string? DepartmentName { get; set; }
        public SelectList? Departments { get; set; }
        public List<Instructor>? Instructors { get; set; }
        public List<CrsResult>? CrsResults { get; set; }
    }
}
