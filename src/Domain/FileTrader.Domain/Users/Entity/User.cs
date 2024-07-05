using FileTrader.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography

namespace FileTrader.Domain.Users.Entity
{
    /// <summary>
    /// Класс для сущности "Пользователь".
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }



    }
}
