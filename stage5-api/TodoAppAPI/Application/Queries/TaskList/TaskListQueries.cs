using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TodoAppAPI.Application.Queries.QueriesTypeHandlers;

namespace TodoAppAPI.Application.Queries.TaskList
{
    public class TaskListQueries : ITaskListQueries
    {
        private IDbConnection _connection;
        public TaskListQueries(IDbConnection connection)
        {
            _connection = connection;

            SqlMapper.AddTypeHandler(new StringCollectionTypeHandler());
        }

        public async Task<IList<TaskListViewModel>> GetAllTaskListAsync()
        {
            string query = "SELECT id AS 'Id', task_name AS'TaskName', task_details AS 'TaskDetails', email AS 'Email', created_at AS'CreatedAt', last_modified AS'LastModified' FROM tasklist";

            var result = await _connection.QueryAsync<TaskListViewModel>(query);

            return result.ToList();
        }

        public async Task<TaskListViewModel> GetTaskListAsyncById(int id)
        {
            string query = "SELECT id AS 'Id', task_name AS'TaskName', task_details AS 'TaskDetails', email AS 'Email', created_at AS'CreatedAt', last_modified AS'LastModified' FROM tasklist WHERE id = @id";
            var result = (await _connection.QueryAsync<TaskListViewModel>(query, new { id = id })).FirstOrDefault();

            return result;
        }
    }
}
