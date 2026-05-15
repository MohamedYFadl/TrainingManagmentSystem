using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC02.Interfaces;
using MVC02.Models;
using MVC02.Repositories;
using MVC02.Specifications;
using MVC02.Specifications.DepartmentSpecs;
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


        public async Task<IActionResult> Index(InputSpecsParams inputSpecsParams)
        {
            ViewBag.Search = inputSpecsParams.Search;

            var countSpecs = new DeptCountSpecs(inputSpecsParams);
            int count = await _deptRepo.GetCountAsync(countSpecs);

            var departmentSpecs = new DepartmentSpecification(inputSpecsParams);
            var departments = await _deptRepo.GetAllAsync(departmentSpecs);

            if(departments == null)
            {
                return NotFound();
            };
            var courses = await _courseRepo.GetAllAsync();
            var instructors = await _instructorRepo.GetAllAsync();
            var trainees = await _traineeRepo.GetAllAsync();

            var DeptVm = departments
                .Select(d => new DeptVm
                {
                    Id = d.Id,
                    Name = d.Name,
                    Manager = d.Manager,
                    Courses = courses.Where(c=>c.DeptId == d.Id).ToList(),
                    Instructors = instructors.Where(c => c.DeptId == d.Id).ToList(),
                    Trainees = trainees.Where(c => c.DeptId == d.Id).ToList(),
                }).ToList();
            var pagination = new PaginatedList<DeptVm>(DeptVm, count, inputSpecsParams.PageIndex, inputSpecsParams.PageSize);
            return View(pagination);
        }
        public async Task<IActionResult> Details(int id)
        {
            var department = await _deptRepo.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            var courses = await _courseRepo.GetAllAsync();
            var instructors = await _instructorRepo.GetAllAsync();
            var trainees = await _traineeRepo.GetAllAsync();

            var departmentVm = new DeptVm 
            {
                Id= id,
                Name= department.Name,
                Manager = department.Manager,
                Courses = courses.Where(c=>c.DeptId ==id).ToList(),
                Instructors = instructors.Where(i=>i.DeptId==id).ToList(),
                Trainees = trainees.Where(t=>t.DeptId==department.Id).ToList()
            };

            return View(departmentVm);
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            DeptVm vm = new DeptVm();
            if (id.HasValue)
            {
                Department department = await _deptRepo.GetByIdAsync(id.Value);
                if (department == null)
                    return NotFound();

                vm.Id = department.Id;
                vm.Name = department.Name;
                vm.Manager = department.Manager;
            }
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DeptVm vm)
        {
                if (vm.Id != 0)
                {
                    Department dept = await _deptRepo.GetByIdAsync(vm.Id);
                    if (dept == null)
                        return NotFound();
                    dept.Name = vm.Name;
                    dept.Manager = vm.Manager;
                    await _deptRepo.UpdateAsync(dept);

                    return RedirectToAction("Index");
                }
                else
                {
                    Department dept = new Department
                    {
                        Name = vm.Name,
                        Manager = vm.Manager
                    };
                    await _deptRepo.AddAsync(dept);
                    return RedirectToAction("Index");
                }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dept = await _deptRepo.GetByIdAsync(id.Value);
            if (dept == null) return NotFound();
            var courses = await _courseRepo.GetAllAsync();
            var instructors = await _instructorRepo.GetAllAsync();
            var trainees = await _traineeRepo.GetAllAsync();

            var departmentVm = new DeptVm
            {
                Id = id.Value,
                Name = dept.Name,
                Manager = dept.Manager,
                Courses = courses.Where(c => c.DeptId == id.Value).ToList(),
                Instructors = instructors.Where(i => i.DeptId == id.Value).ToList(),
                Trainees = trainees.Where(t => t.DeptId == id.Value).ToList()
            };
            return View(departmentVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dept = await _deptRepo.GetByIdAsync(id);
            if (dept != null) await _deptRepo.RemoveAsync(dept.Id);
            TempData["Success"] = "Department deleted successfully!";
            return RedirectToAction(nameof(Index));
        }


    }
}
