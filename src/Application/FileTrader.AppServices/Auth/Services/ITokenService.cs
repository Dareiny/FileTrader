using FileTrader.Contracts.Users;
using System.IdentityModel.Tokens.Jwt;

namespace FileTrader.AppServices.Auth.Services
{
    /// <summary>
    /// Сервис для работы с Jwt токеном.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Генерация токена.
        /// </summary>
        /// <param name="userData">Данные пользователя</param>
        /// <param name="key">Jwt ключ</param>
        /// <returns>Jwt токен <see cref="JwtSecurityToken"/>.</returns>
        public Task<JwtSecurityToken> GenerateToken(UserDTO userData, byte[] key);

        /// <summary>
        /// Получение логина из токена.
        /// </summary>
        /// <param name="token">Jwt токен.</param>
        /// <param name="key">Jwt ключ.</param>
        /// <returns>Логин.</returns>
        public Task<String> GetLoginFromToken(string token, byte[] key);

        /// <summary>
        /// Получение Id из токена
        /// </summary>
        /// <param name="token">Jwt токен.</param>
        /// <param name="key">Jwt ключ.</param>
        /// <returns>Id.</returns>
        public Task<String> GetIdFromToken(string token, byte[] key);
    }

}
