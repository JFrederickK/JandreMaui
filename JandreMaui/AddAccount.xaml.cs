using JandreMaui.ViewModel;

namespace JandreMaui;

public partial class AddAccount : ContentPage
{
    public AccountViewModel accountViewModel;
    public AddAccount(AccountViewModel addaccountViewMode)
	{
		this.BindingContext = accountViewModel = addaccountViewMode;
		InitializeComponent();
	}
}