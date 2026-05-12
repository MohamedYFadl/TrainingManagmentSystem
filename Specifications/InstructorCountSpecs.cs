using MVC02.Models;

namespace MVC02.Specifications
{
    public class InstructorCountSpecs : BaseSpecification<Instructor>
    {
        public InstructorCountSpecs(InputSpecsParams inputParams)
            : base(c =>
                string.IsNullOrEmpty(inputParams.Search) ||
                c.Name.ToLower().Contains(inputParams.Search.ToLower()))
        {
        }
    }
}
