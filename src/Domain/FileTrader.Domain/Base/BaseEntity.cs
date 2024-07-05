using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.Domain.Base
{
    /// <summary>
    /// Базовый класс для всех сущностей.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTime CreatedDate { get; set; }

    }
}
