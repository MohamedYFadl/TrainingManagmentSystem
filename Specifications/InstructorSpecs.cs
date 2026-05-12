using MVC02.Models;

namespace MVC02.Specifications
{
    public class InstructorSpecs:BaseSpecification<Instructor>
    {
        public InstructorSpecs(InputSpecsParams input)
            :base(i=>string.IsNullOrEmpty(input.Search) || i.Name.ToLower().Contains(input.Search.ToLower()))
        {
            Includes.Add(i=>i.Course);
            Includes.Add(i=>i.Department);
        }
    }
}
