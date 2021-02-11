using Domain.AggregatesModel.ItemListAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class ItemListDetailsUpdatedDomainEvent : INotification
    {
        public ItemListAggregateModel OriginalTaskList { get; set; }
        public ItemListAggregateModel UpdatedTaskList { get; set; }

        public ItemListDetailsUpdatedDomainEvent(ItemListAggregateModel originalTaskList, ItemListAggregateModel updatedTaskList)
        {
            OriginalTaskList = originalTaskList;
            UpdatedTaskList = updatedTaskList;
        }
    }
}
