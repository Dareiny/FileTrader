namespace FileTrader.Contracts.Users
{
    /// <summary>
    /// Модель пользователя без пароля.
    /// </summary>
    public class UserInfoDTO
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
    }
}
