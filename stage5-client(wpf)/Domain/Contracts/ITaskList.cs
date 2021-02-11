using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ITaskList : IBaseRepo<TaskListModel>
    {
        public IEnumerable<TaskListModel> FindByFK(string token);


        int CreateWitKey(TaskListModel entity, string token, string key);
    }
}
