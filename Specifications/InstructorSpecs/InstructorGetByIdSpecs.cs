using MVC02.Models;

namespace MVC02.Specifications.InstructorSpecs
{
    public class InstructorGetByIdSpecs:BaseSpecification<Instructor>
    {
        public InstructorGetByIdSpecs(int id)
            : base(i => i.Id == id)
        {
            Includes.Add(i => i.Course);
            Includes.Add(i => i.Department);
        }
    }
}
