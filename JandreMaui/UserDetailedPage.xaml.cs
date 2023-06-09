using JandreMaui.ViewModel;

namespace JandreMaui;

public partial class UserDetailedPage : ContentPage
{

    public DetailAccountViewModel DetailAccountViewModel;
    public UserDetailedPage(DetailAccountViewModel detailAccount)
	{
		InitializeComponent();
        this.BindingContext = DetailAccountViewModel = detailAccount;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Delay(1000).ContinueWith(t => this.DetailAccountViewModel.ReadFromFile());
    }
}