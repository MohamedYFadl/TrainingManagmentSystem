using MVC02.Interfaces;
using MVC02.Models;

namespace MVC02.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly ITIDBContext _context;

        public CourseRepository(ITIDBContext context) : base(context)
        {
            _context = context;
        }
        
        
    }
}