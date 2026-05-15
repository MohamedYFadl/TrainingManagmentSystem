using MVC02.Models;

namespace MVC02.Specifications
{
    public class DepartmentSpecification:BaseSpecification<Department>
    {
        public DepartmentSpecification(InputSpecsParams input):base
            (d => string.IsNullOrEmpty(input.Search) || d.Name.ToLower().Contains(input.Search.ToLower())
            || string.IsNullOrEmpty(input.Search) || d.Manager.ToLower().Contains(input.Search.ToLower()))
        {
            //Includes.Add(d => d.Courses);
            //Includes.Add(d => d.Instructors);
            //Includes.Add(d => d.Trainees);
            ApplyPagaination(input.PageSize * (input.PageIndex - 1), input.PageSize);
        }
    }
}
