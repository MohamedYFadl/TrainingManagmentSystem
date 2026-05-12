using Microsoft.EntityFrameworkCore;

namespace MVC02.Specifications
{
    public static class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specs)
        {
            var query = inputQuery;
            if (specs.Criteria != null)
            {
                query = query.Where(specs.Criteria);
            }

            if (specs.OrderBy != null)
                query = query.OrderBy(specs.OrderBy);

            else if (specs.OrderByDesc != null)
                query = query.OrderByDescending(specs.OrderByDesc);

            if (specs.IsPagainationEnable)
                query = query.Skip(specs.Skip).Take(specs.Take);

            query = specs.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;
        }
    }

}
