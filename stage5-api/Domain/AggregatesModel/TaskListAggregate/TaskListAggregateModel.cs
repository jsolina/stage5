using Domain.Events;
using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.TaskListAggregate.cs
{
    public class TaskListAggregateModel : Entity<int>, IAggregateRoot
    {
        public TaskListAggregateModel(string taskName, string taskDetails, string email, DateTime createdAt)
        {
            TaskName = taskName;
            TaskDetails = taskDetails;
            Email = email;
            CreatedAt = createdAt;
            LastModified = createdAt; // Set the last modified date same to created date
        }
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public string Email { get; set; }

        public void UpdateDetails(string taskName, string taskDetails, string email, DateTime lastModified)
        {
            var originalAccountCopy = this.GetCopy() as TaskListAggregateModel;

            TaskName = taskName;
            TaskDetails = taskDetails;
            Email = email;
            LastModified = lastModified;

            AddDomainEvent(new TaskListDetailsUpdatedDomainEvent(originalAccountCopy, this));
        }

        public void SoftDelete(DateTime lastModified)
        {
            LastModified = lastModified;

            AddDomainEvent(new TaskListAddedDomainEvent(Id, true));
        }

    }

}
