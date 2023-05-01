using JandreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JandreMaui.LocalDatabases
{
    public class AccountService : IAccount
    {
        public Task DeleteAccount(UserAccounts accounts)
        {
            throw new NotImplementedException();
        }

        public Task GetAccount(UserAccounts accounts)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserAccounts>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public Task SaveAccount(UserAccounts accounts)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAccount(UserAccounts accounts)
        {
            throw new NotImplementedException();
        }
    }
}
