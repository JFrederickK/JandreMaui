using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JandreMaui.Models;
using JandreMaui.LocalDatabases;

namespace JandreMaui.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        ILocalDataBaseRepository _localDataBaseRepository;
        public MainViewModel(ILocalDataBaseRepository localDataBaseRepository)
        {

            //Onthou om die service te inject.
            Items = new ObservableCollection<ToDoClass>();
            this.ReadFromFile().ConfigureAwait(false);
            this._localDataBaseRepository = localDataBaseRepository; 

        }

        ToDoClass selectedToDo;
        public ToDoClass SelectedToDo
        {
            get
            {
                return selectedToDo;
            }
            set
            {
                if (selectedToDo != value)
                {
                    selectedToDo = value;
                    this.OnPropertyChanged(nameof(SelectedToDo));

                    this.Tap(selectedToDo).ConfigureAwait(false);
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<ToDoClass> items;

        [ObservableProperty]
        private string newTask;

        [RelayCommand]
        private async Task Add()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.NewTask))
                {
                    return;
                }
                ToDoClass toDoClass = new ToDoClass()
                {
                    Name = this.NewTask,

                };
                 this.WriteToFile(toDoClass);

                this.NewTask = string.Empty;

                await this.ReadFromFile();
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Error at the add button", exc.Message, "Ok");
            }

        }

        private async Task Tap(ToDoClass reference)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?reference={reference.Id}");
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Error on the tap conmmand", exc.Message, "Ok");

            }

        }

        private string RandomId()
        {
            Random random = new Random();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                sb.Append(letters[random.Next(letters.Length)]);
            }
            for (int i = 0; i < 3; i++)
            {
                sb.Append(numbers[random.Next(numbers.Length)]);
            }
            return sb.ToString();

        }

        [RelayCommand]
        public async Task ReadFromFile()
        {
            try
            {
                await Task.Delay(500);
                List<ToDoClass> results = await this._localDataBaseRepository.GetItemsAsync();
                if (results != null)
                {
                    this.Items = new ObservableCollection<ToDoClass>(results);
                }
                else {
                    this.Items = null;
                }
                
                
            }
            catch (Exception exc)
            {
                await Shell.Current.DisplayAlert("Read File", exc.Message, "Ok");
            }

        }

        private  async void WriteToFile(ToDoClass items)
        {
           await this._localDataBaseRepository.SaveItemAsync(items);
        }

    }
}
