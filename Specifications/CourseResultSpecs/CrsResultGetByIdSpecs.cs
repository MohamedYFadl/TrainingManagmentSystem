using MVC02.Models;

namespace MVC02.Specifications.CourseResultSpecs
{
    public class CrsResultGetByIdSpecs: BaseSpecification<CrsResult>
    {
        public CrsResultGetByIdSpecs(int id):base(r => r.Id == id)
        {
            Includes.Add(r => r.Course);
            Includes.Add(r => r.Trainee);
        }
    }
}
