namespace FileTrader.Contracts.General
{
    /// <summary>
    /// Запрос на получение всех пользователей.
    /// </summary>
    public class PaginationRequest
    {
        /// <summary>
        /// Номер страницы списка пользователей (1 по стандарту).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Количество записей на странице (10 по стандарту).
        /// </summary>
        public int BatchSize { get; set; } = 10;

    }
}
