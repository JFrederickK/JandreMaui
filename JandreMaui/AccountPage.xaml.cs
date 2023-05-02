using JandreMaui.LocalDatabases;
using JandreMaui.ViewModel;

namespace JandreMaui;

public partial class AccountPage : ContentPage
{
	public AccountViewModel accountViewModel;
	readonly IAccount userAccount;
	public AccountPage(AccountViewModel accountViewModel, IAccount account)
	{
		InitializeComponent();
		this.BindingContext = this.accountViewModel = accountViewModel;
		userAccount = account;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Task.Delay(500).ContinueWith(t => this.accountViewModel.ReadAccounts());
    }
}