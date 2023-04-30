
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
		Task.Delay(500).ContinueWith(t => this.viewModel.ReadFromFile());
    }
    async void Tasks_SelectionChanged(Object sender,SelectionChangedEventArgs e)
    {
        await Navigation.PushAsync(new DetailsPage(e.CurrentSelection.FirstOrDefault() as ToDoClass));
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}

