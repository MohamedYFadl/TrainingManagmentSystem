using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC02.Interfaces;
using MVC02.Models;
using MVC02.Repositories;
using MVC02.Specifications;
using MVC02.ViewModels;

namespace MVC02.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository instructorRepo;
        private readonly ICourseRepository courseRepo;
        private readonly IDepartmentRepository departmentRepo;
        private readonly IWebHostEnvironment _env;

        public InstructorController(
            IInstructorRepository instructorRepo,
            ICourseRepository courseRepo,
            IDepartmentRepository departmentRepo,
            IWebHostEnvironment env)
        {
            this.instructorRepo = instructorRepo;
            this.courseRepo = courseRepo;
            this.departmentRepo = departmentRepo;
            _env = env;
        }
        public async Task<IActionResult> Index(string? search, string? sort, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.Search = search;
            var InputSpecsParams = new InputSpecsParams()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Search = search,    
                Sort = sort
            };
            var countSpecs = new InstructorCountSpecs(InputSpecsParams);
            var count = await instructorRepo.GetCountAsync(countSpecs);

            var insSpecs =  new InstructorSpecs(InputSpecsParams);
            var instructors = await instructorRepo.GetAllAsync(insSpecs);
            if(instructors == null)
                return NotFound();  

            var insVm = instructors.Select(i => new InsViewModel
            {
                Id = i.Id,
                InsName = i.Name,
                InsSalary = i.Salary,
                InsAddress = i.Address,
                CourseName = i.Course.Name,
                DepartmentName = i.Department.Name,
                ImgeUrl = i.ImageUrl
            }).ToList();
            var pagination = new PaginatedList<InsViewModel>(insVm, count, pageIndex, pageSize);

            return View(pagination);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var instructor = await instructorRepo.GetByIdAsync(id);
            if(instructor == null)
                return NotFound();

            InsViewModel ins = new InsViewModel
            {
                Id = instructor.Id,
                InsName = instructor.Name,
                InsSalary = instructor.Salary,
                InsAddress = instructor.Address,
                CourseName = instructor.Course.Name,
                DepartmentName = instructor.Department.Name,
                ImgeUrl = instructor.ImageUrl
            };
            return View(ins);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsCrsDepVm input,IFormFile imageFile)
        {
            ModelState.Remove("ImageUrl");
            if (ModelState.IsValid)
            {
                Instructor ins = new Instructor()
                {
                    Name = input.InsName,
                    Salary = input.InsSalary,
                    Address = input.InsAddress,
                    ImageUrl = await SaveImageAsync(imageFile),
                    DeptId = input.DeptId,
                    Crs_id = input.CrsId
                };

               await instructorRepo.AddAsync(ins);
                return RedirectToAction("Index");
            }
            await PopulateDropdowns();
            return View(input);
        }
        private async Task PopulateDropdowns(Instructor? instructor = null)
        {
            ViewBag.Departments = new SelectList(await departmentRepo.GetAllAsync(), "Id", "Name", instructor?.DeptId);
            ViewBag.Courses = new SelectList(await courseRepo.GetAllAsync(), "Id", "Name", instructor?.Crs_id);
        }
        private async Task<string?> SaveImageAsync(IFormFile? imageFile, string? existingImageUrl = null)
        {
            if (imageFile == null || imageFile.Length == 0)
                return existingImageUrl; // keep existing if no new file

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!allowed.Contains(ext))
                return existingImageUrl;

            var uploadFolder = Path.Combine(_env.WebRootPath, "images", "instructors");
            Directory.CreateDirectory(uploadFolder);

            // Delete old file if replacing
            if (!string.IsNullOrEmpty(existingImageUrl))
            {
                var oldPath = Path.Combine(_env.WebRootPath, existingImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadFolder, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return $"/images/instructors/{fileName}";
        }

    }
}
