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
