using Domain.AggregatesModel.ItemListAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class ItemListAddedDomainEvent : INotification
    {
        public ItemListAggregateModel ItemList { get; set; }

        public ItemListAddedDomainEvent(ItemListAggregateModel itemList)
        { 
            ItemList = itemList;
        }
    }
}
