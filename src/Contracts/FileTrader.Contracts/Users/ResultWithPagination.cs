using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.Contracts.Users
{
    /// <summary>
    /// Результат запроса с пагинацией.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultWithPagination<T>
    {
        /// <summary>
        /// Список записей на странице.
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        /// <summary>
        /// Количество оставшихся страниц.
        /// </summary>
        public int AvailablePages { get; set; }
    }
}
