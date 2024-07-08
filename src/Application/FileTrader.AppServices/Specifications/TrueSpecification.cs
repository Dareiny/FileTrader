using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
