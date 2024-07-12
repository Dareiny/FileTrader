using FileTrader.Domain.Base;

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
