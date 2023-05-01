using JandreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JandreMaui.LocalDatabases
{
    internal interface IAccount
    {
        public Task<List<UserAccounts>> GetAllAccounts();

        public Task GetAccount(UserAccounts accounts);

        public Task DeleteAccount(UserAccounts accounts);

        public Task UpdateAccount(UserAccounts accounts);

        public Task SaveAccount(UserAccounts accounts);

    }
}
