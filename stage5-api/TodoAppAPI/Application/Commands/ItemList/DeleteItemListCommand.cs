using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Commands.ItemList
{
    public class DeleteItemListCommand : BaseCommand, IRequest<bool>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
