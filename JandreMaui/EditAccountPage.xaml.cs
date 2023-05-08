using JandreMaui.ViewModel;

namespace JandreMaui;

public partial class EditAccountPage : ContentPage
{
	public EditAccountViewModel EditAccountViewModel;
	public EditAccountPage(EditAccountViewModel editAccountViewModel)
	{
		this.InitializeComponent();
		this.BindingContext =  EditAccountViewModel = editAccountViewModel;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}