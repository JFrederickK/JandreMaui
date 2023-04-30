using JandreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JandreMaui.LocalDatabases
{
    public interface ILocalDataBaseRepository
    {
        Task<List<ToDoClass>> GetItemsAsync();

        Task<ToDoClass> GetItemAsync(int id);

        Task SaveItemAsync(ToDoClass item);

        Task DeleteItemAsync(ToDoClass item);
    }
}
