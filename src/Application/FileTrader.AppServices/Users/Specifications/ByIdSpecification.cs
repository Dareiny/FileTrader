using FileTrader.AppServices.Specifications;
using FileTrader.Domain.Users.Entity;
using System.Linq.Expressions;

namespace FileTrader.AppServices.Users.Specifications
{
    /// <summary>
    /// Спецификация для проверки Id пользователя.
    /// </summary>
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
