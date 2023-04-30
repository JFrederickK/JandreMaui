using JandreMaui.Models;
using JandreMaui.ViewModel;

namespace JandreMaui;

public partial class DetailsPage : ContentPage
{
	public DetailViewModel detailViewModel;

    public DetailsPage( ToDoClass toDo)
	{
		InitializeComponent();
		this.BindingContext = toDo;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Delay(500).ContinueWith(t => this.detailViewModel.ReadFromFile());
    }
}