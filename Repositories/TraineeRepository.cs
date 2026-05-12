using MVC02.Interfaces;
using MVC02.Models;

namespace MVC02.Repositories
{
    public class TraineeRepository : GenericRepository<Trainee>, ITraineeRepository
    {
        private readonly ITIDBContext context;

        public TraineeRepository(ITIDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
