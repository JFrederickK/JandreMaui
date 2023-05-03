using JandreMaui.HttpConfiguration;
using JandreMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

        public async Task<UserAccounts> GetAccount(UserAccounts accounts)
        {
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            HttpClient client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            client = new HttpClient();
#endif
            var results = await client.GetAsync($"https://localhost:7270/api/AccountUser/{accounts.Id}");
            var resp = await results.Content.ReadAsStringAsync();
            UserAccounts userAccounts = JsonConvert.DeserializeObject<UserAccounts>(resp);
            return userAccounts;

        }

        public async Task<List<UserAccounts>> GetAllAccounts()
        {

#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            HttpClient client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            client = new HttpClient();
#endif
            var results = await client.GetAsync($"https://10.0.2.2:7270/api/AccountUser");
            var resp = await results.Content.ReadAsStringAsync(); 
            List<UserAccounts> accounts = JsonConvert.DeserializeObject<List<UserAccounts>>(resp);
            return accounts;
        }

        public async Task SaveAccount(UserAccounts accounts)
        {
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            HttpClient client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            client = new HttpClient();
#endif
            var results = ($"https://10.0.2.2:7270/api/AccountUser");
            HttpResponseMessage respones = await client.PostAsJsonAsync(results, accounts);
        }

        public Task UpdateAccount(UserAccounts accounts)
        {
            throw new NotImplementedException();
        }
    }
}
