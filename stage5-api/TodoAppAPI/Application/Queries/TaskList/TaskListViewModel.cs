using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Queries.TaskList
{
    [Serializable]
    public class TaskListViewModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}
