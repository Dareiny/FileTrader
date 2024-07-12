namespace FileTrader.Contracts.UserFiles
{
    /// <summary>
    /// Запрос на изменение доступа к файлу
    /// </summary>
    public class UpdateAccessRequest
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Доступ к файлу.
        /// </summary>
        public bool GeneralAccess { get; set; }

    }
}
