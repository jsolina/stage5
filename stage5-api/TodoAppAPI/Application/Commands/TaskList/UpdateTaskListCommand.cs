using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Commands.TaskList
{
    public class UpdateTaskListCommand : BaseCommand, IRequest<bool>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public string Email { get; set; }
    }
}
