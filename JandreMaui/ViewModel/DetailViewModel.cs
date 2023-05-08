using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JandreMaui.LocalDatabases;
using JandreMaui.Models;


namespace JandreMaui.ViewModel
{
    [QueryProperty(nameof(Reference), "reference")]
    public partial class DetailViewModel : ObservableObject
    {
        #region Constructor
        ILocalDataBaseRepository _localDataBaseRepository;
        public DetailViewModel(ILocalDataBaseRepository localDataBaseRepository)
        {

            //Onthou om die service te inject.
            Items = new ToDoClass();
            this._localDataBaseRepository = localDataBaseRepository;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Old school method om die value the set wat van n ander page af kom
        /// </summary>
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
        int reference;        
        #endregion


        #region Instant Fields
        [ObservableProperty]
        private ToDoClass items;

        [ObservableProperty]
        private string taskDescription;
        #endregion

        #region Relay Command
        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Delete()
        {
            try
            {
                await this._localDataBaseRepository.DeleteItemAsync(this.Items);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Could not delete the task", exc.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task Edit()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.TaskDescription))
                {
                    return;
                }
                ToDoClass newToDoClass = new ToDoClass()
                {
                    Description = this.TaskDescription,
                    Name = this.Items.Name,
                    Id = this.Items.Id
                };
                await this._localDataBaseRepository.SaveItemAsync(newToDoClass);
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Could not edit the task description", exc.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task Tap()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ToDoEditePage)}?reference={this.Reference}");
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Error on the tap conmmand", exc.Message, "Ok");

            }

        }
        #endregion

        #region Instant Fields
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
                    StartTime = results.StartTime,
                    EndTime = results.EndTime,
                };
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Read File \nCould not find the task", exc.Message, "Ok");
            }

        }
        #endregion


    }

}
