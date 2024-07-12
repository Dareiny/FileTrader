namespace FileTrader.Contracts.UserFiles
{
    /// <summary>
    /// Модель информации о файле.
    /// </summary>
    public class FileInfoDTO
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; set; }

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
