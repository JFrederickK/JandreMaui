using JandreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JandreMaui.LocalDatabases
{
    public interface IAccount
    {
        public Task<List<UserAccounts>> GetAllAccounts();

        public Task<UserAccounts> GetAccount(UserAccounts accounts);

        public Task DeleteAccount(UserAccounts accounts);

        public Task UpdateAccount(UserAccounts accounts);

        public Task SaveAccount(UserAccounts accounts);

    }
}
