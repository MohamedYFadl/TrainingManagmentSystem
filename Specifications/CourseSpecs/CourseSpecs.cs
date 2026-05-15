using MVC02.Models;

namespace MVC02.Specifications
{
    public class CourseSpecs:BaseSpecification<Course>
    {
        public CourseSpecs(InputSpecsParams inputParams)
            :base(
            c => string.IsNullOrEmpty(inputParams.Search) || c.Name.ToLower().Contains(inputParams.Search.ToLower())
            || string.IsNullOrEmpty(inputParams.Search) || c.Department.Name.ToLower().Contains(inputParams.Search.ToLower()))
        {
            Includes.Add(c=>c.Department);
            if (!string.IsNullOrEmpty(inputParams.Sort))
            {
                switch (inputParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    case "hoursAsc":
                        AddOrderBy(P => P.Hours);
                        break;
                    case "hoursDesc":
                        AddOrderByDesc(P => P.Hours);
                        break;
                    case "departmentAsc":
                        AddOrderBy(p => p.Department.Name);
                        break;
                    case "departmentDesc":
                        AddOrderByDesc(p => p.Department.Name);
                        break;
                    default:
                        AddOrderBy(p => p.Id);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Id);

            }

            ApplyPagaination((inputParams.PageIndex - 1) * inputParams.PageSize,
            inputParams.PageSize);
        }
    }
}
