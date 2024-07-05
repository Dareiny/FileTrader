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
        public string UserName { get; set; }
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string UserEmail { get; set; }

    }
}
