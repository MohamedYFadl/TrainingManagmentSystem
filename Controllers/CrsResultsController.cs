using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC02.Interfaces;
using MVC02.Models;
using MVC02.Specifications;
using MVC02.Specifications.CourseResult;
using MVC02.Specifications.CourseResultSpecs;
using MVC02.ViewModels;
using System.Threading.Tasks;

namespace MVC02.Controllers
{
    //[Route("CrsResults")]
    public class CrsResultsController : Controller
    {
        private readonly IGenericRepository<CrsResult> _crsRepo;
        private readonly ITraineeRepository _traineeRepository;
        private readonly ICourseRepository _courseRepository;

        public CrsResultsController(
            IGenericRepository<CrsResult> crsRepo,
            ITraineeRepository traineeRepository,
            ICourseRepository courseRepository)
        {
            _crsRepo = crsRepo;
            _traineeRepository = traineeRepository;
            _courseRepository = courseRepository;
        }
        //[HttpGet("{traineeId:int?}/{courseId:int?}")]
        public async Task<IActionResult> Index(string? search,int? traineeId,int? courseId, int pageIndex = 1, int pageSize = 5)
         {
            var CrsResultSpecs = new CrsResultSpecsParams
            {
                PageIndex = pageIndex,
                PageSize= pageSize,
                Search = search,
                CourseId = courseId,
                TraineeId = traineeId,
            };

            var countSpecs = new CrsResultCountSpecs(CrsResultSpecs);

            var count = await _crsRepo.GetCountAsync(countSpecs);

            var crsResultSpecs = new CrsResultSpecification(CrsResultSpecs);

            var CrsResult = await _crsRepo.GetAllAsync(crsResultSpecs);

            var CrsResultVm = CrsResult.Select(c => new CrsResultViewModel
            {
                Id = c.Id,
                Degree = c.Degree,
                CourseName = c.Course.Name,
                TraineeName = c.Trainee.Name,
                MinDegree = c.Course.MinDegree
            }).ToList();

            var paingation = new PaginatedList<CrsResultViewModel>(CrsResultVm, count, pageIndex,pageSize);

            return View(paingation);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var crsResultSpecs = new CrsResultGetByIdSpecs(id);
            var result = await _crsRepo.GetByIdAsync(crsResultSpecs);
            if (result == null) return NotFound();
            var crsResultVm = new CrsResultViewModel
            {
                Id = result.Id,
                Degree = result.Degree,
                CourseName = result.Course.Name,
                TraineeName = result.Trainee.Name,
                MinDegree = result.Course.MinDegree
            };
            return View(crsResultVm);
        }

        private async Task PopulateDropdowns(CrsResult? result = null)
        {
            var courses = await _courseRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name", result?.Crs_Id);

            var trainees = await _traineeRepository.GetAllAsync();
            ViewBag.Trainees = new SelectList(trainees, "Id", "Name", result?.TraineeId);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrsResult crsResult)
        {
            if (ModelState.IsValid)
            {
               await _crsRepo.AddAsync(crsResult);
                TempData["Success"] = "Course Result created successfully!";
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns(crsResult);
            return View(crsResult);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _crsRepo.GetByIdAsync(new CrsResultGetByIdSpecs(id));
            if (result == null) return NotFound();
            await PopulateDropdowns(result);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CrsResult crsResult)
        {
            if (id != crsResult.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await   _crsRepo.UpdateAsync(crsResult);
                    TempData["Success"] = "Course Result updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _crsRepo.GetByIdAsync(new CrsResultGetByIdSpecs(id)) == null) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns(crsResult);
            return View(crsResult);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crsRepo.GetByIdAsync(new CrsResultGetByIdSpecs(id));
            if (result == null) return NotFound();
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _crsRepo.GetByIdAsync(new CrsResultGetByIdSpecs(id));
            if (result != null) await _crsRepo.RemoveAsync(id);
            TempData["Success"] = "Course Result deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
