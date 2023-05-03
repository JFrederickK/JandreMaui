
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

        builder.Services.AddSingleton<ILocalDataBaseRepository, DatabaseService>();
		builder.Services.AddSingleton<IAccount , AccountService>();
		builder.Services.AddTransient<AddAccount>();
		builder.Services.AddTransient<UserDetailPage>();

		builder.Services.AddTransient<AccountPage>();
		builder.Services.AddTransient<AccountViewModel>();

		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<MainPage>();

        builder.Services.AddTransient<DetailViewModel>();
        builder.Services.AddTransient<DetailsPage>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
