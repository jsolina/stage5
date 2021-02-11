using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class TaskListDeletedDomainEvent : INotification
    {
        public int BatchPayoutId { get; set; }
        public bool Deleted { get; set; }


        public TaskListDeletedDomainEvent(int batchPayoutId, bool deleted)
        {
            BatchPayoutId = batchPayoutId;
            Deleted = deleted;
        }
    }
}
