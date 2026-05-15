using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC02.Interfaces;
using MVC02.Models;
using MVC02.Specifications;
using MVC02.ViewModels;

namespace MVC02.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IGenericRepository<CrsResult> _crsResultRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private const int PageSize = 5;

        public CourseController(
            ICourseRepository courseRepository,
            IInstructorRepository instructorRepository,
            IGenericRepository<CrsResult> crsResultRepository,
            IDepartmentRepository departmentRepository)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _crsResultRepository = crsResultRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            CourseDeptVm vm = new CourseDeptVm();
            vm.Departments = new SelectList(await _departmentRepository.GetAllAsync(), "Id", "Name");
            if (id.HasValue)
            {
                var courseSpec = new CourseWithIdSpecs(id.Value);
                Course course = await _courseRepository.GetByIdAsync(courseSpec);
                if (course == null)
                    return NotFound();

                vm.Id = course.Id;
                vm.Name = course.Name;
                vm.Degree = course.Degree;
                vm.MinDegree = course.MinDegree;
                vm.Hours = course.Hours;
                vm.DeptId = course.DeptId;
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(CourseDeptVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departments = new SelectList(_courseRepository.GetAllAsync().Result, "Id", "Name");
                return View(vm);
            }
            else
            {
                if (vm.Id != 0)
                {
                    var courseSpec = new CourseWithIdSpecs(vm.Id);
                    Course course = await _courseRepository.GetByIdAsync(courseSpec);
                    if (course == null)
                        return NotFound();
                    course.Name = vm.Name;
                    course.Degree = vm.Degree;
                    course.MinDegree = vm.MinDegree;
                    course.Hours = vm.Hours;
                    course.DeptId = vm.DeptId;
                    await _courseRepository.UpdateAsync(course);
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    Course course = new Course
                    {
                        Name = vm.Name,
                        Degree = vm.Degree,
                        MinDegree = vm.MinDegree,
                        Hours = vm.Hours,
                        DeptId = vm.DeptId
                    };
                   await _courseRepository.AddAsync(course);
                    return RedirectToAction("Index");
                }

            }
        }
        public async Task<IActionResult> Index(string? search,string? sort , int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.Search = search;
            ViewBag.Sort = sort;

            var courseSpecParams = new InputSpecsParams()
            {
                Search = search,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Sort = sort,
            };
            // 
            var countSpec = new CourseCountSpecs(courseSpecParams);
            var courseSpec = new CourseSpecs(courseSpecParams);
            var courses = await _courseRepository.GetAllAsync(courseSpec);
            var count = await _courseRepository.GetCountAsync(countSpec);
            if (courses == null)
                return NotFound();

            var instructors = await _instructorRepository.GetAllAsync();
                    
            var courseDeptVms = courses.Select(c => new CourseDeptVm
            {
                Id = c.Id,
                Name = c.Name,
                Degree = c.Degree,
                MinDegree = c.MinDegree,
                Hours = c.Hours,
                DeptId = c.DeptId,
                DepartmentName = c.Department.Name,
                Instructors = instructors.Where(i => i.Crs_id == c.Id).ToList()
            }).ToList();
            var pagination = new PaginatedList<CourseDeptVm>(courseDeptVms, count, pageIndex, pageSize);
            return View(pagination);
        }

        public IActionResult LessThanDegree(int MinDegree, int Degree)
        {
            if (MinDegree < Degree)
                return Json(true);

            return Json("min degree must be less than degree");
        }
        public async Task<IActionResult> Details(int id)
        {
            var courseSpec = new CourseWithIdSpecs(id);
            Course course = await _courseRepository.GetByIdAsync(courseSpec);
            if (course == null)
                return NotFound();

            var instructors = await _instructorRepository.GetAllAsync();
            var crsResults = await _crsResultRepository.GetAllAsync();
            var courseVm = new CourseDeptVm
            {
                Id = course.Id,
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                Hours = course.Hours,
                DeptId = course.DeptId,
                DepartmentName = course.Department.Name,
                Instructors = instructors.Where(i => i.Crs_id == course.Id).ToList(),
                CrsResults = crsResults.Where(r => r.Crs_Id == course.Id).ToList()
            };
            return View(courseVm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var courseSpec = new CourseWithIdSpecs(id);
            Course course = await _courseRepository.GetByIdAsync(courseSpec);
            if (course == null)
                return NotFound();
            var courseVm = new CourseDeptVm
            {
                Id = course.Id,
                Name = course.Name,
            };
            return View(courseVm);
        }
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSpec = new CourseWithIdSpecs(id);
            Course course = await _courseRepository.GetByIdAsync(courseSpec);
            if (course == null)
                return NotFound();
            await _courseRepository.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
