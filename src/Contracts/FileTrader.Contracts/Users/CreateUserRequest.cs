using System.ComponentModel.DataAnnotations;

namespace FileTrader.Contracts.Users
{
    /// <summary>
    /// Модель для создания записи пользователя
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Login { get; set; }
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        [Required]
        [StringLength(256)]
        public string UserEmail { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [StringLength(30)]
        [Required]
        public string Password { get; set; }
    }
}
