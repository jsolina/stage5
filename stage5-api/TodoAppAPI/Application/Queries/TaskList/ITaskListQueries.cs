using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Queries.TaskList
{
    public interface ITaskListQueries
    {
        Task<IList<TaskListViewModel>> GetAllTaskListAsync();

        Task<TaskListViewModel> GetTaskListAsyncById(int id);
    }
}
