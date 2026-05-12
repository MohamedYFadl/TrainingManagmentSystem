using MVC02.Models;

namespace MVC02.Specifications
{
    public class TraineeSpecs:BaseSpecification<Trainee>
    {
        public TraineeSpecs()
        {
            Includes.Add(t=>t.Department);
        }
    }
}
