using JandreMaui.ViewModel;

namespace JandreMaui;

public partial class ToDoEditePage : ContentPage
{
	public EditeToDoViewModel viewModel;
	public ToDoEditePage(EditeToDoViewModel editeViewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel = editeViewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Delay(500).ContinueWith(t => this.viewModel.ReadFromFile());
    }
}