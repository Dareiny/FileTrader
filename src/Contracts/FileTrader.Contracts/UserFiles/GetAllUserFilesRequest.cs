using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.Contracts.Users
{
    /// <summary>
    /// Запрос на получение всех пользователей.
    /// </summary>
    public class GetAllUserFilesRequest
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
