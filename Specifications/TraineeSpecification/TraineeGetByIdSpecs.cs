using MVC02.Models;

namespace MVC02.Specifications.TraineeSpecs
{
    public class TraineeGetByIdSpecs:BaseSpecification<Trainee>
    {
        public TraineeGetByIdSpecs(int id):base(t=>t.Id == id)
        {
            Includes.Add(t => t.Department);
        }
    }
}
