using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC02.Interfaces;
using MVC02.Models;
using MVC02.ViewModels;
using MVC02.Specifications.TraineeSpecification;
using MVC02.Specifications.CourseResult;

namespace TrainingMVC.Controllers
{
    public class TraineesController : Controller
    {

        public TraineesController(
            ITraineeRepository traineeRepository,
            IGenericRepository<Department> DeptRepo,
            IGenericRepository<CrsResult> crsResultRepo,
            IWebHostEnvironment env)
        {
            this.traineeRepository = traineeRepository;
            deptRepo = DeptRepo;
            this.crsResultRepo = crsResultRepo;
            this.env = env;
        }
        private readonly ITraineeRepository traineeRepository;
        private readonly IGenericRepository<Department> deptRepo;
        private readonly IGenericRepository<CrsResult> crsResultRepo;
        private readonly IWebHostEnvironment env;

        public async Task<IActionResult> Index(InputSpecsParams input)
        {
            ViewBag.Search = input.Search;
            
            int count = await traineeRepository.GetCountAsync(new TraineeCountSpecs(input));
            var TraineeSpecs = new TraineeSpecs(input);
            var trainees = await traineeRepository.GetAllAsync(TraineeSpecs);

            var pagination = new PaginatedList<Trainee>(trainees, count, input.PageIndex,input.PageSize);
            return View(pagination);
        }

        public async Task<IActionResult> Details(int id)
        {
            var traineeSpecs = new MVC02.Specifications.TraineeSpecs.TraineeGetByIdSpecs(id);
            var trainee = await traineeRepository.GetByIdAsync(traineeSpecs);
            if (trainee == null) return NotFound();

            var crsResultSpecs = new CrsResultSpecification(new CrsResultSpecsParams());
            var CrsResults = await crsResultRepo.GetAllAsync(crsResultSpecs);
            var traineeVm = new TraineeViewModel
            {
                Address = trainee.Address,
                DepartmentName = trainee.Department.Name,
                Name = trainee.Name,
                DeptId = trainee.DeptId,
                Grade = trainee.Grade,
                Id = trainee.Id,
                ImageUrl = trainee.ImageUrl,
                CrsResults = CrsResults.Where(c => c.TraineeId == trainee.Id).ToList(),
            };
            return View(traineeVm);
        }

        private async Task PopulateDropdowns(Trainee? trainee = null)
        {
            ViewBag.Departments = new SelectList(await deptRepo.GetAllAsync(), "Id", "Name", trainee?.DeptId);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TraineeVmRequest input,IFormFile imageFile)
        {
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                var trainee = new Trainee
                {
                    Name = input.Name,
                    Address = input.Address,
                    Grade = input.Grade,
                    DeptId = input.DeptId,
                    ImageUrl = await SaveImageAsync(imageFile)
                };
                await traineeRepository.AddAsync(trainee);
                TempData["Success"] = "Trainee created successfully!";
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns();
            return View(input);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var trainee = await traineeRepository.GetByIdAsync(id);
            if (trainee == null) return NotFound();
            var traineeVm = new TraineeVmRequest()
            {
                Id = trainee.Id,
                Address = trainee.Address,
                DeptId = trainee.DeptId,
                Grade = trainee.Grade,
                Name = trainee.Name,
                ImageUrl = trainee.ImageUrl
            };
            await PopulateDropdowns(trainee);
            return View(traineeVm); // Pass the view model, not the entity
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TraineeVmRequest trainee, IFormFile? imageFile)
        {
            if (trainee == null) return NotFound();
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                try
                {
                   var existing =  await traineeRepository.GetByIdAsync(trainee.Id);
                    existing.ImageUrl = await SaveImageAsync(imageFile, existing.ImageUrl);
                    await traineeRepository.UpdateAsync(existing);
                    TempData["Success"] = "Trainee updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await traineeRepository.GetByIdAsync(trainee.Id) == null) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns();
            return View(trainee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var trainee = await traineeRepository.GetByIdAsync(id.Value);
            if (trainee == null) return NotFound();
            return View(trainee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainee = await traineeRepository.GetByIdAsync(id);
            if (trainee != null) await traineeRepository.RemoveAsync(trainee.Id);
            TempData["Success"] = "Trainee deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private async Task<string?> SaveImageAsync(IFormFile? imageFile, string? existingImageUrl = null)
        {
            if (imageFile == null || imageFile.Length == 0)
                return existingImageUrl; // keep existing if no new file

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!allowed.Contains(ext))
                return existingImageUrl;

            var uploadFolder = Path.Combine(env.WebRootPath, "images", "instructors");
            Directory.CreateDirectory(uploadFolder);

            // Delete old file if replacing
            if (!string.IsNullOrEmpty(existingImageUrl))
            {
                var oldPath = Path.Combine(env.WebRootPath, existingImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadFolder, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return $"/images/trainees/{fileName}";
        }

    }
}
