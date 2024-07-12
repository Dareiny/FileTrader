using FileTrader.AppServices.Specifications;
using FileTrader.Domain.Files.Entity;
using System.Linq.Expressions;

namespace FileTrader.AppServices.UserFiles.Specifications
{
    /// <summary>
    /// Спецификация для проверки id владельца.
    /// </summary>
    public class ByOwnerIdSpecification : Specification<EFile>
    {
        private readonly Guid _ownerId;
        public ByOwnerIdSpecification(Guid ownerId)
        {
            _ownerId = ownerId;
        }

        public override Expression<Func<EFile, bool>> ToExpression()
        {
            return file => file.OwnerId == _ownerId;
        }
    }
}
