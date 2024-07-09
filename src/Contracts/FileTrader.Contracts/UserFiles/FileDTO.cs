using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.Contracts.UserFiles
{
    /// <summary>
    /// Модель файла.
    /// </summary>
    public class FileDTO
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
