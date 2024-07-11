using FileTrader.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.Domain.Files.Entity
{
    /// <summary>
    /// Сущность файла.
    /// </summary>
    public class UserFile : BaseEntity
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Контент файла.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Тип контента.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Длина файла.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Идентификатор владельца.
        /// </summary>
        public Guid OwnerId { get; set; }

        public bool GeneralAccess { get; set; }
    }
}
