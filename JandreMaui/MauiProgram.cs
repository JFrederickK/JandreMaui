using JandreMaui.LocalDatabases;
using JandreMaui.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JandreMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddTransient<ILocalDataBaseRepository, DatabaseService>();
		builder.Services.AddTransient<IAccount , AccountService>();

		builder.Services.AddTransient<AddAccount>();
		builder.Services.AddTransient<EditToDoViewModel>();
		builder.Services.AddTransient <ToDoEditePage> ();

		builder.Services.AddTransient<EditAccountPage>();
		builder.Services.AddTransient<EditAccountViewModel>();

		builder.Services.AddTransient<UserDetailedPage>();
		builder.Services.AddTransient<DetailAccountViewModel>();

		builder.Services.AddTransient<AccountPage>();
		builder.Services.AddTransient<AccountViewModel>();

		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<MainPage>();

        builder.Services.AddTransient<DetailViewModel>();
        builder.Services.AddTransient<DetailsPage>();


        builder.Services.AddTransient<NewPage1>();



        



#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
