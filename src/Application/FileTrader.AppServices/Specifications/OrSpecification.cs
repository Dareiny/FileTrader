using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.AppServices.Specifications
{
    public class OrSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        private readonly Specification<TEntity> _left;
        private readonly Specification<TEntity> _right;

        public OrSpecification(Specification<TEntity> left, Specification<TEntity> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var parameter = Expression.Parameter(typeof(TEntity));
            var orExpression = Expression.OrElse(
                Expression.Invoke(leftExpression, parameter),
                Expression.Invoke(rightExpression, parameter)
            );

            return Expression.Lambda<Func<TEntity, bool>>(orExpression, parameter);
        }
    }
}
