using MVC02.Interfaces;
using MVC02.Models;

namespace MVC02.Repositories
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        private readonly ITIDBContext context;

        public InstructorRepository(ITIDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
