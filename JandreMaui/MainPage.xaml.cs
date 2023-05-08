
using JandreMaui.LocalDatabases;
using JandreMaui.Models;
using JandreMaui.ViewModel;
using System.Numerics;

namespace JandreMaui;

public partial class MainPage : ContentPage
{

	public MainViewModel viewModel;
    readonly ILocalDataBaseRepository database;

    public MainPage(MainViewModel viewModel, ILocalDataBaseRepository toDoDatabase)
	{
		InitializeComponent();
		this.BindingContext = this.viewModel = viewModel;
		database = toDoDatabase;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		Task.Delay(1000).ContinueWith(t => this.viewModel.ReadFromFile());
    }

    private async void OnYoMama(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new NewPage1());
    }
}

