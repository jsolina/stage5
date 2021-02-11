using Domain.Contracts;
using Domain.Models;
using Infrastracture.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastracture.Repositories
{
    public class TaskListRepo : ITaskList
    {
        TaskListRest _dbcontext = new TaskListRest();

        public void Create(TaskListModel entity, string token)
        {
            _dbcontext.PostRequest(entity, token);
        }

        public int CreateWitKey(TaskListModel entity, string token, string key)
        {
            return _dbcontext.PostRequestWithKey(entity, token, key);
        }

        public IEnumerable<TaskListModel> FindAll(string token)
        {
            return _dbcontext.GetRequestWithToken(token);
        }

        public IEnumerable<TaskListModel> FindByFK(string token)
        {
            throw new NotImplementedException();
        }

        public TaskListModel FindById(int id, string token)
        {
            return _dbcontext.GetByIdRequest(id, token);
        }

        public void Remove(TaskListModel entity, string token)
        {
            _dbcontext.DeleteRequest(entity, token);
        }

        public void Update(TaskListModel entity, string token)
        {
            _dbcontext.PutRequest(entity, token);
        }
    }
}
