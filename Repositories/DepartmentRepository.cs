using MVC02.Interfaces;
using MVC02.Models;

namespace MVC02.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ITIDBContext context;

        public DepartmentRepository(ITIDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
