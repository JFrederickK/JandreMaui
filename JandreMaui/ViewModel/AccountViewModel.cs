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

        UserAccounts selectedToDo;
        public UserAccounts SelectedToDo
        {
            get
            {
                return selectedToDo;
            }
            set
            {
                if (selectedToDo != value)
                {
                    selectedToDo = value;
                    this.OnPropertyChanged(nameof(SelectedToDo));

                    this.Tap(selectedToDo).ConfigureAwait(false);
                }
            }
        }


        [ObservableProperty]
        private ObservableCollection<UserAccounts> useraccounts;

        [ObservableProperty]
        private string newUserName;

        [ObservableProperty]
        private string newUserSurname;

        [ObservableProperty]
        private string newUserEmail;

        [RelayCommand]
        public async Task ReadAccounts()
        {
            try
            {
                await Task.Delay(1000);
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

                await Shell.Current.DisplayAlert("Could not read user account", exc.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task TapAdd()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(AddAccount)}");
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Error on the tap conmmand", exc.Message, "Ok");

            }

        }
        [RelayCommand]
        private async Task Add()
        {
            try
            {
                UserAccounts newUser = new UserAccounts()
                {
                    Name = this.NewUserName,
                    Surname = this.NewUserSurname,
                    Email = this.NewUserEmail,
                    IsActive = true,
                };
               await this.account.SaveAccount(newUser);
                await GoBack();
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Error at the add button", exc.Message, "Ok");
            }
        }
        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task Tap(UserAccounts reference)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?reference={reference.Id}");
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Error on the tap conmmand", exc.Message, "Ok");

            }

        }
    }
}
