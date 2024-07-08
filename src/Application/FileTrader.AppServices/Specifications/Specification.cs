using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.AppServices.Specifications
{
    public abstract class Specification<TEntity> where TEntity : class
    {
        public bool IsSatisfiedBy(TEntity entity)
        {
            Func<TEntity, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public Specification<TEntity> And(Specification<TEntity> specification)
        {
            return new AndSpecification<TEntity>(this, specification);
        }
        public Specification<TEntity> Or(Specification<TEntity> specification)
        {
            return new OrSpecification<TEntity>(this, specification);
        }
        public Specification<TEntity> Not(Specification<TEntity> specification)
        {
            return new NotSpecification<TEntity>(this);
        }
    }
}
