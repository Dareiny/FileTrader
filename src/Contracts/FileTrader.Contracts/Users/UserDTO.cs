using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.Contracts.Users
{
    /// <summary>
    /// DTO для пользователя.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? Login { get; set; }
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string? UserEmail { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string? Password { get; set; }

    }
}
