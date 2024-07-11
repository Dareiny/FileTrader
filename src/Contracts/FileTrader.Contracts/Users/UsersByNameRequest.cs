using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.Contracts.Users
{
    /// <summary>
    /// Запрос на получение пользователей по имени
    /// </summary>
    public class UsersByNameRequest
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Login { get; set; }
    }
}
