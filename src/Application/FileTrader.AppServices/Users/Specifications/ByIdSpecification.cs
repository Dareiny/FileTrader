using FileTrader.AppServices.Specifications;
using FileTrader.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.AppServices.Users.Specifications
{
    public class ByIdSpecification : Specification<User>
    {
        private readonly Guid _id;
        public ByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Id == _id;
        }
    }
}
