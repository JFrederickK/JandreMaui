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
    {//https://localhost:7270/api/AccountUser/1
        public async Task DeleteAccount(int accounts)
        {
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            HttpClient client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            client = new HttpClient();
#endif
            string results = ($"https://10.0.2.2:7270/api/AccountUser/{accounts}");
            HttpResponseMessage response = await client.DeleteAsync(results);
            UserAccounts userAccounts = JsonConvert.DeserializeObject<UserAccounts>(await response.Content.ReadAsStringAsync());
        }

        public async Task<UserAccounts> GetAccount(int accounts)
        {
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            HttpClient client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            client = new HttpClient();
#endif
            var results = await client.GetAsync($"https://10.0.2.2:7270/api/AccountUser/{accounts}");
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

        public async Task UpdateAccount(int id, UserAccounts accounts)
        {
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            HttpClient client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            client = new HttpClient();
#endif
            var results = ($"https://10.0.2.2:7270/api/AccountUser/{id}");
            HttpResponseMessage response = await client.PutAsJsonAsync(results, accounts);
            UserAccounts userAccounts = JsonConvert.DeserializeObject<UserAccounts>(await response.Content.ReadAsStringAsync());

        }
    }
}
