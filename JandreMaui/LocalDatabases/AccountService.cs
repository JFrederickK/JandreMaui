using JandreMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        public async Task<List<UserAccounts>> GetAllAccounts()
        {

            using var clients = new HttpClient();
            var results = await clients.GetAsync($"http://localhost:5265/api/AccountUser");
            var resp = await results.Content.ReadAsStringAsync(); 
            List<UserAccounts> accounts = JsonConvert.DeserializeObject<List<UserAccounts>>(resp);
            return accounts;
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
