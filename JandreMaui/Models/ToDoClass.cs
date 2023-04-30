using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace JandreMaui.Models
{
    [Table("ToDo")]
    public class ToDoClass
    {
        #region Constructor
        public ToDoClass()
        {

        }

        #endregion
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
