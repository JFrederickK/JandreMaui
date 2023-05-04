namespace JandreMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
		Routing.RegisterRoute(nameof(AddAccount), typeof(AddAccount));
		Routing.RegisterRoute(nameof(UserDetailedPage), typeof(UserDetailedPage));
	}
}
