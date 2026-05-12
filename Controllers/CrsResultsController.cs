using Microsoft.AspNetCore.Mvc;
using MVC02.Interfaces;
using MVC02.Models;
using MVC02.Specifications;
using MVC02.ViewModels;
using System.Threading.Tasks;

namespace MVC02.Controllers
{
    public class CrsResultsController : Controller
    {
        private readonly IGenericRepository<CrsResult> _crsRepo;

        public CrsResultsController(IGenericRepository<CrsResult> crsRepo)
        {
            _crsRepo = crsRepo;
        }
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

            var paingation = new PaginatedList<CrsResultViewModel>(CrsResultVm, count, pageIndex, pageSize);

            return View(paingation);
        }
    }
}
