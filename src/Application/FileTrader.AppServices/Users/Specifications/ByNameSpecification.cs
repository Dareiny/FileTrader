using FileTrader.AppServices.Specifications;
using FileTrader.Domain.Users.Entity;
using System.Linq.Expressions;

namespace FileTrader.AppServices.Users.Specifications
{
    /// <summary>
    /// Спецификация для проверки имени пользователя
    /// </summary>
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
