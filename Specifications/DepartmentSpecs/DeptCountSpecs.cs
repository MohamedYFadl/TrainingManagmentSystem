using MVC02.Models;

namespace MVC02.Specifications.DepartmentSpecs
{
    public class DeptCountSpecs:BaseSpecification<Department>
    {
        public DeptCountSpecs(InputSpecsParams input) : base
            (d => string.IsNullOrEmpty(input.Search) || d.Name.ToLower().Contains(input.Search.ToLower())
            || string.IsNullOrEmpty(input.Search) || d.Manager.ToLower().Contains(input.Search.ToLower()))
        {
        }
    }
}
