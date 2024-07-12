namespace FileTrader.Contracts.General
{
    /// <summary>
    /// Результат запроса с пагинацией.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ResultWithPagination<TModel>
    {
        /// <summary>
        /// Список записей на странице.
        /// </summary>
        public IEnumerable<TModel> Result { get; set; }

        /// <summary>
        /// Количество оставшихся страниц.
        /// </summary>
        public int AvailablePages { get; set; }
    }
}
