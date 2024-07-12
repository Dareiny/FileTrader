using System.Linq.Expressions;

namespace FileTrader.AppServices.Specifications
{
    public class NotSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        private readonly Specification<TEntity> _specification;

        public NotSpecification(Specification<TEntity> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var expression = _specification.ToExpression();

            var parameter = Expression.Parameter(typeof(TEntity));
            var notExpression = Expression.Not(Expression.Invoke(expression, parameter));

            return Expression.Lambda<Func<TEntity, bool>>(notExpression, parameter);
        }
    }
}
