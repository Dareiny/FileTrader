using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Добавляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Обновляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Удаляет записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

    }
}
