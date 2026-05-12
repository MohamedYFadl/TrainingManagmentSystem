using Microsoft.AspNetCore.Mvc;
using MVC02.Interfaces;
using MVC02.ViewModels;

namespace MVC02.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _deptRepo;
        private readonly IInstructorRepository _instructorRepo;
        private readonly ITraineeRepository _traineeRepo;
        private readonly ICourseRepository _courseRepo;

        public DepartmentController(
            IDepartmentRepository deptRepo,
            IInstructorRepository instructorRepo,
            ITraineeRepository traineeRepo,
            ICourseRepository courseRepo)
        {
            _deptRepo = deptRepo;
            _instructorRepo = instructorRepo;
            _traineeRepo = traineeRepo;
            _courseRepo = courseRepo;
        }


        public async Task<IActionResult> Index()
        {
            var departments = await _deptRepo.GetAllAsync();
            if(departments == null)
            {
                return NotFound();
            };
            var DeptVm = departments
                .Select(d => new DeptVm
                {
                    Id = d.Id,
                    Name = d.Name,
                    Manager = d.Manager,
                    Courses = _courseRepo.GetAllAsync().Result.Where(c=>c.DeptId == d.Id).ToList(),
                    Instructors = _instructorRepo.GetAllAsync().Result.Where(i => i.DeptId == d.Id).ToList(),
                    Trainees = _traineeRepo.GetAllAsync().Result.Where(t => t.DeptId == d.Id).ToList()
                }).ToList();

            if(departments == null )
                return NotFound();
            return View(DeptVm);
        }
    }
}
