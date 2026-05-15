using MVC02.Models;

namespace MVC02.Specifications.TraineeSpecification
{
    public class TraineeCountSpecs:BaseSpecification<Trainee>
    {
        public TraineeCountSpecs(InputSpecsParams inputParams)
            : base(c =>
                string.IsNullOrEmpty(inputParams.Search) ||
                c.Name.ToLower().Contains(inputParams.Search.ToLower()))
        {
        }
    }
}
