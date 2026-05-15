using MVC02.Models;

namespace MVC02.Specifications.TraineeSpecification
{
    public class TraineeSpecs:BaseSpecification<Trainee>
    {
        public TraineeSpecs(InputSpecsParams input)
           : base(
            c => string.IsNullOrEmpty(input.Search) || c.Name.ToLower().Contains(input.Search.ToLower())
            || string.IsNullOrEmpty(input.Search) || c.Department.Name.ToLower().Contains(input.Search.ToLower()))
        {
            Includes.Add(t=>t.Department);
            ApplyPagaination((input.PageIndex - 1) * input.PageSize,
            input.PageSize);
        }
    }
}
