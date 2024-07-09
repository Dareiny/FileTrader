using FileTrader.Contracts.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace FileTrader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            //TODO
            var result = request;

            return await Task.Run(() => CreatedAtAction(nameof(RegisterAccount), result),cancellationToken);
        }

        /// <summary>
        /// Вход в сессию.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            //TODO
            var result = request;

            return await Task.Run(() => Ok(result), cancellationToken);
        }

        /// <summary>
        /// Выход из сессии.
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
        }

        [HttpPost("user-info")]
        public async Task<AccountDTO> GetUserInfo(CancellationToken cancellationToken)
        {
            //TODO
            var result = new AccountDTO();

            return result;
        }
    }
}
