using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JandreMaui.LocalDatabases;
using JandreMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JandreMaui.ViewModel
{
    public partial class AccountViewModel : ObservableObject
    {
        IAccount account;
        public AccountViewModel(IAccount userAccount) 
        {
            Useraccounts = new ObservableCollection<UserAccounts>();
            this.ReadAccounts().ConfigureAwait(false);
            this.account = userAccount;
        }

        [ObservableProperty]
        private ObservableCollection<UserAccounts> useraccounts;

        [RelayCommand]
        public async Task ReadAccounts()
        {
            try
            {
                List<UserAccounts> users = await this.account.GetAllAccounts();
                if (users != null)
                {
                    this.Useraccounts = new ObservableCollection<UserAccounts>(users);
                }
                else
                {
                    this.Useraccounts = null;
                }
            }
            catch (Exception exc)
            {

                await Shell.Current.DisplayAlert("Could not read user account",exc.Message, "Ok");
            }
        }
    }
}
