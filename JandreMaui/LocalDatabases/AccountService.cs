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
        /// <summary>
        /// Used to remove a account given their Id
        /// </summary>
        /// <param name="accounts"></param>

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

        /// <summary>
        /// Used to get a single account base on the given Id
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Used to get all the available accounts
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Used to add an acvount
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Used to update an account base on the Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
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
