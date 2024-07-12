using FileTrader.Domain.Base;

namespace FileTrader.Domain.Files.Entity
{
    /// <summary>
    /// Сущность файла.
    /// </summary>
    public class EFile : BaseEntity
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

        /// <summary>
        /// Доступ к файлу.
        /// </summary>
        public bool GeneralAccess { get; set; }
    }
}
