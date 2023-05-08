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
    [QueryProperty(nameof(Reference), "reference")]
    public partial class EditAccountViewModel : ObservableObject
    {

        IAccount userAccount;
        public EditAccountViewModel(IAccount userAccountEdit)
        { 
            items = new UserAccounts();
            this.userAccount = userAccountEdit;
        }
        public int Reference
        {
            get { return this.reference; }
            set
            {
                if (this.reference != value)
                {
                    this.reference = value;
                    this.OnPropertyChanged(nameof(Reference));
                    this.ReadFromFile().ConfigureAwait(false);
                }
            }
        }

        int reference;

        [ObservableProperty]
        private UserAccounts items;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string userSurname;

        [ObservableProperty]
        private string userEmail;

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task Edit()
        {
            try
            {

                await this.userAccount.UpdateAccount(this.Reference, new UserAccounts()
                {
                    Name = this.UserName != null ? this.UserName : Items.Name,
                    Email = this.UserEmail != null ? this.UserEmail : Items.Email,
                    Surname = this.UserSurname != null ? this.UserSurname : Items.Surname,
                    Id = this.Reference,
                    IsActive = true
                });
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Could not update user", exc.Message, "Ok");
            }
        }
        public async Task ReadFromFile()
        {
            try
            {
                int id = this.Reference;
                var results = await this.userAccount.GetAccount(id);
                this.Items = new UserAccounts()
                {
                    Email = results.Email,
                    Id = results.Id,
                    IsActive = results.IsActive,
                    Name = results.Name,
                    Surname = results.Surname

                };
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Read File \nCould not find the task", exc.Message, "Ok");
            }

        }
    }
}
