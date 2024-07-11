using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.Contracts.Accounts
{
    public class AccountLoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
