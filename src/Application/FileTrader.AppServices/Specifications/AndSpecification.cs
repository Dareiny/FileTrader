using System.Linq.Expressions;

namespace FileTrader.AppServices.Specifications
{
    public class AndSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        private readonly Specification<TEntity> _left;
        private readonly Specification<TEntity> _right;

        public AndSpecification(Specification<TEntity> left, Specification<TEntity> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var parameter = Expression.Parameter(typeof(TEntity));
            var andExpression = Expression.AndAlso(
                Expression.Invoke(leftExpression, parameter),
                Expression.Invoke(rightExpression, parameter)
            );

            return Expression.Lambda<Func<TEntity, bool>>(andExpression, parameter);
        }
    }
}
