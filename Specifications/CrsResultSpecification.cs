using MVC02.Models;

namespace MVC02.Specifications
{
    public class CrsResultSpecification:BaseSpecification<CrsResult>
    {
        public CrsResultSpecification(CrsResultSpecsParams input):base( 
            c=> (string.IsNullOrEmpty(input.Search) || c.Course.Name.ToLower().Contains(input.Search.ToLower()))
            || (string.IsNullOrEmpty(input.Search) || c.Trainee.Name.ToLower().Contains(input.Search.ToLower()))
             && (!input.TraineeId.HasValue || c.TraineeId==input.TraineeId)
             && (!input.CourseId.HasValue || c.Crs_Id==input.CourseId)
             )
        {
            Includes.Add(c=>c.Course);
            Includes.Add(c=>c.Trainee);
        }
    }
}
