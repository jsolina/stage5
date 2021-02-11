using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Commands.ItemList
{
    public class AddItemListCommand : BaseCommand, IRequest<AddItemListResult>
    {
        public int IdTask { get; set; }
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public string ItemStatus { get; set; }
    }

    public class AddItemListDTO
    {
        public int IdTask { get; set; }
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public string ItemStatus { get; set; }
    }

    public class AddItemListResult
    {
        public AddItemListResult(int idTask, string itemName, string itemDetails, string itemStatus)
        {
            IdTask = idTask;
            ItemName = itemName;
            ItemDetails = itemDetails;
            ItemStatus = itemStatus;
        }

        public int IdTask { get; set; }
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public string ItemStatus { get; set; }
    }

}
