using System.Linq.Expressions;

namespace FileTrader.AppServices.Specifications
{
    public class TrueSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return entity => true;
        }
    }
}
