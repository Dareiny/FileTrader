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
    public class ByNameSpecification : Specification<User>
    {
        private readonly string _name;
        public ByNameSpecification(string name) 
        {
            _name = name;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Login == _name;
        }
    }
}
