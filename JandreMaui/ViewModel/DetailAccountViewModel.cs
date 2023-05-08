﻿using CommunityToolkit.Mvvm.ComponentModel;
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

    public partial class DetailAccountViewModel : ObservableObject
    {
        #region Constructor
        IAccount userAccount;
        public DetailAccountViewModel(IAccount account) 
        {
            items = new UserAccounts();
            this.userAccount = account;
        }
        #endregion

        #region Properties
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
        #endregion

        #region Instant Fields
        [ObservableProperty]
        private UserAccounts items;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string userSurname;

        [ObservableProperty]
        private string userEmail;
        #endregion

        #region Relay Command
        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task Delete()
        {
            try
            {
                await this.userAccount.DeleteAccount(this.Reference);
                await Shell.Current.GoToAsync("..");

            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Could not delete", exc.Message, "Ok");
            }

        }
        [RelayCommand]
        public async Task Tap()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(EditAccountPage)}?reference={this.Reference}");
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Error on the tap conmmand", exc.Message, "Ok");

            }

        }
        #endregion

        #region Instant Methods
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
                await Shell.Current.DisplayAlert("Read File \nCould not find the user", exc.Message, "Ok");
            }

        }
        #endregion

    }
}
