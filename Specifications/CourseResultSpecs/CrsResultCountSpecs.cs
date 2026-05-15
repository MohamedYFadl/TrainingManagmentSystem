using MVC02.Models;

namespace MVC02.Specifications
{
    public class CrsResultCountSpecs:BaseSpecification<CrsResult>
    {

        public CrsResultCountSpecs(CrsResultSpecsParams inputParams)
            : base(c =>
                string.IsNullOrEmpty(inputParams.Search) ||
                c.Course.Name.ToLower().Contains(inputParams.Search.ToLower()))
        {
        }

    }
}
