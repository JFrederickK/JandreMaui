using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JandreMaui.LocalDatabases;
using JandreMaui.Models;

namespace JandreMaui.ViewModel
{
    [QueryProperty(nameof(Reference), "reference")]
    public partial class EditToDoViewModel : ObservableObject
    {
        public int Reference
        {
            get { return this.reference; }
            set
            {
                if (this.reference != value)
                {
                    this.reference = value;
                    this.OnPropertyChanged(nameof(Reference));
                    this.ReadFromFile().ConfigureAwait(false);
                }
            }
        }
        ILocalDataBaseRepository _localDataBaseRepository;
        public EditToDoViewModel(ILocalDataBaseRepository localDataBaseRepository)
        {

            //Onthou om die service te inject.
            Items = new ToDoClass();
            this._localDataBaseRepository = localDataBaseRepository;
        }

        [ObservableProperty]
        private ToDoClass items;

        [ObservableProperty]
        private string taskDescription;

        [ObservableProperty]
        private string newTaskName;

        int reference;

        [RelayCommand]
       public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task Edit()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.TaskDescription) && String.IsNullOrWhiteSpace(this.NewTaskName))
                {
                    return;
                }
                ToDoClass newToDoClass = new ToDoClass()
                {
                    Description = this.TaskDescription,
                    Name = this.NewTaskName,
                    Id = this.Items.Id,
                    StartTime = DateTime.Now,
                };
                await this._localDataBaseRepository.SaveItemAsync(newToDoClass);
                await GoBack();
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Could not edit the task description", exc.Message, "Ok");
            }
        }
        public async Task ReadFromFile()
        {
            try
            {
                int id = this.Reference;
                var results = await this._localDataBaseRepository.GetItemAsync(id);
                this.Items = new ToDoClass()
                {
                    Id = results.Id,
                    Name = results.Name,
                    Description = results.Description,

                };
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Read File \nCould not find the task", exc.Message, "Ok");
            }

        }
    }
}
