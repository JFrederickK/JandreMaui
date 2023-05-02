using JandreMaui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace JandreMaui.LocalDatabases
{
    public class DatabaseService : ILocalDataBaseRepository
    {
        public SQLiteAsyncConnection _connection;
        public DatabaseService()
        {
            if (this.sqlConnection == null)
            {
                this.CreateConnection().ConfigureAwait(false);
            }

            this.sqlConnection.CreateTableAsync<ToDoClass>(CreateFlags.None);
        }

        public async Task DeleteItemAsync(ToDoClass item)
        {
            try
            {
                if (this.sqlConnection == null)
                {
                    throw new ArgumentNullException(nameof(this.sqlConnection));
                }
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                var exixtingToDo = await this.sqlConnection.Table<ToDoClass>().FirstOrDefaultAsync(x => x.Id == item.Id);
                await this.sqlConnection.Table<ToDoClass>().DeleteAsync(x => x.Id == exixtingToDo.Id);
                return;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ToDoClass> GetItemAsync(int id)
        {
            try
            {
                if (this.sqlConnection == null)
                {
                    throw new ArgumentNullException(nameof(this.sqlConnection));
                }
                var capture = await this.sqlConnection.FindAsync<ToDoClass>(id);
                var convertedCapture = new ToDoClass()
                {
                    Description = capture.Description,
                    Name = capture.Name,
                    Id = capture.Id,
                    EndTime = capture.EndTime,
                    StartTime = capture.StartTime,

                };
                return convertedCapture;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ToDoClass>> GetItemsAsync()
        {
            try
            {
                if (this.sqlConnection == null)
                {
                    throw new ArgumentNullException(nameof(this.sqlConnection));
                }
                var capture = await this.sqlConnection.Table<ToDoClass>().OrderByDescending(x => x.StartTime).ToListAsync();
                return capture;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SaveItemAsync(ToDoClass item)
        {
            try
            {
                if (this.sqlConnection == null)
                {
                    throw new ArgumentNullException(nameof(this.sqlConnection));
                }
                var captureTable = this.sqlConnection.Table<ToDoClass>();

                var existingCapture = await captureTable.Where(x => x.Id == item.Id).FirstOrDefaultAsync();

                if (existingCapture == null)
                {
                    var addedCap = await this.sqlConnection.InsertAsync(new ToDoClass()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        StartTime = DateTime.Now,
                        EndTime = item.EndTime,

                    }, typeof(ToDoClass));

                }
                else
                {
                    existingCapture.Name = item.Name;
                    existingCapture.Description = item.Description;
                    existingCapture.Id = item.Id;
                    existingCapture.StartTime = item.StartTime;
                    existingCapture.EndTime = item.EndTime;

                    await this.sqlConnection.UpdateAsync(existingCapture);
                }
                return;

            }
            catch (Exception exc)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new <see cref="SQLiteAsyncConnection"/> to access the locally stored data with.
        /// </summary>
        /// <returns></returns>
        private Task CreateConnection()
        {
            try
            {
                TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

                SQLiteOpenFlags Flags =
                            // open the database in read/write mode
                            SQLiteOpenFlags.ReadWrite |
                            // create the database if it doesn't exist
                            SQLiteOpenFlags.Create |
                            // enable multi-threaded database access
                            SQLiteOpenFlags.SharedCache;
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "AssetPhotoCaptures.db3");
                this.sqlConnection = new SQLiteAsyncConnection(databasePath, Flags);

                return completionSource.Task;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SQLiteAsyncConnection sqlConnection;
    }
}
