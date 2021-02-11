using Domain.Events;
using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.ItemListAggregate
{
    public class ItemListAggregateModel : Entity<int>, IAggregateRoot
    {
        public ItemListAggregateModel(int idTask, string itemName, string itemDetails, string itemStatus, DateTime createdAt)
        {
            IdTask = idTask;
            ItemName = itemName;
            ItemDetails = itemDetails;
            ItemStatus = itemStatus;
            CreatedAt = createdAt;
            LastModified = createdAt; // Set the last modified date same to created date
        }

        public int IdTask { get; set; }
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public string ItemStatus { get; set; }

        public void UpdateDetails(string itemName, string itemDetails, string itemStatus, DateTime lastModified)
        {
            var originalAccountCopy = this.GetCopy() as ItemListAggregateModel;

            ItemName = itemName;
            ItemDetails = itemDetails;
            ItemStatus = itemStatus;
            LastModified = lastModified;

            AddDomainEvent(new ItemListDetailsUpdatedDomainEvent(originalAccountCopy, this));
        }

        public void SoftDelete(DateTime lastModified)
        {
            LastModified = lastModified;

            AddDomainEvent(new TaskListAddedDomainEvent(Id, true));
        }
    }
}
