using Domain.AggregatesModel.TaskListAggregate.cs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class TaskListDetailsUpdatedDomainEvent : INotification
    {
        public TaskListAggregateModel OriginalTaskList { get; set; }
        public TaskListAggregateModel UpdatedTaskList { get; set; }

        public TaskListDetailsUpdatedDomainEvent(TaskListAggregateModel originalTaskList, TaskListAggregateModel updatedTaskList)
        {
            OriginalTaskList = originalTaskList;
            UpdatedTaskList = updatedTaskList;
        }
    }
}
