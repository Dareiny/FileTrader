namespace FileTrader.Contracts.Accounts
{
    /// <summary>
    /// Запрос на авторизацию.
    /// </summary>
    public class AccountLoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
