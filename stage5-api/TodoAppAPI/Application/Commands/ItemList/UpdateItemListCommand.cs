using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Commands.ItemList
{ 
    public class UpdateItemListCommand : BaseCommand, IRequest<bool>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public string ItemStatus { get; set; }
    }
}
