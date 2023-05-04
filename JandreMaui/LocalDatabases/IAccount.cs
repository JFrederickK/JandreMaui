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

        public Task<UserAccounts> GetAccount(int accounts);

        public Task DeleteAccount(int accounts);

        public Task UpdateAccount(int id,UserAccounts accounts);

        public Task SaveAccount(UserAccounts accounts);

    }
}
