using Microsoft.EntityFrameworkCore;
using MVC02.Interfaces;
using MVC02.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MVC02.Models
{
    public class UniquePerDept:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return null;
            var courseRepo = (IGenericRepository<Course>)validationContext.GetService(typeof(IGenericRepository<Course>))!;
            var vm = (CourseDeptVm)validationContext.ObjectInstance;
            var inputName = value.ToString();
            bool exists = courseRepo.GetAllAsync().Result.Any(i => i.Name.Trim().ToLower() == inputName.Trim().ToLower() && i.DeptId == vm.DeptId && i.Id != vm.Id);

            if (exists)
                return new ValidationResult("Course name already exists in this department");

            return ValidationResult.Success;
        }
    }
}
