namespace FileTrader.Contracts.Users
{
    /// <summary>
    /// Модель пользователя.
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
