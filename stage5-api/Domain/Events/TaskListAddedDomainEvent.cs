using Domain.AggregatesModel.TaskListAggregate.cs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;  

namespace Domain.Events
{
    public class TaskListAddedDomainEvent : INotification
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public TaskListAddedDomainEvent(int id, bool deleted)
        {
            Id = id;
            Deleted = deleted;
        }
    }
}
