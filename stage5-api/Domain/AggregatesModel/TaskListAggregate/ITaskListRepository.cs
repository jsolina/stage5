using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregatesModel.TaskListAggregate.cs
{
    public interface ITaskListRepository : IRepository<TaskListAggregateModel>
    {
        TaskListAggregateModel Add(TaskListAggregateModel account);

        void Update(TaskListAggregateModel updatedAccount);

        void Remove(TaskListAggregateModel entity);

        Task<TaskListAggregateModel> GetAsync(int idTask);
    }
}
