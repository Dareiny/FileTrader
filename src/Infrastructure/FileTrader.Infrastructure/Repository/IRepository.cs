using System.Linq.Expressions;

namespace FileTrader.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Возвращает все элементы сущности <see cref="TEntity"/>.
        /// </summary>
        /// <returns>Все элементы сущности<see cref="TEntity"/>.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Возвращает все элементы сущности <see cref="TEntity"/> по предикату.
        /// </summary>
        /// <param name="predicate">Предикат</param>
        /// <returns>Все элементы сущности <see cref="TEntity"/> по предикату.</returns>
        IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Возвращает все элементы сущности <see cref="TEntity"/> по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns><see cref="TEntity"/>.</returns>
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <returns></returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    }
}
