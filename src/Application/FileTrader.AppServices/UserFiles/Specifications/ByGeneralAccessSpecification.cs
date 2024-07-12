using FileTrader.AppServices.Specifications;
using FileTrader.Domain.Files.Entity;
using System.Linq.Expressions;

namespace FileTrader.AppServices.UserFiles.Specifications
{
    /// <summary>
    /// Спецификация для проверки доступа.
    /// </summary>
    public class ByGeneralAccessSpecification : Specification<EFile>
    {
        private readonly bool _generalAccess;
        public ByGeneralAccessSpecification(bool generalAccess)
        {
            _generalAccess = generalAccess;
        }

        public override Expression<Func<EFile, bool>> ToExpression()
        {
            return file => file.GeneralAccess == _generalAccess;
        }
    }
}
