using MVC02.Models;

namespace MVC02.Specifications
{
    public class CourseWithIdSpecs:BaseSpecification<Course>
    {
        public CourseWithIdSpecs(int id):base(p=>p.Id == id)
        {
            Includes.Add(c=>c.Department);
        }
    }
}
