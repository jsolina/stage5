using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TodoAppAPI.Authentication;

namespace TodoAppAPI.Application.Commands
{
    public class BaseCommand
    {
        [JsonIgnore]
        public AuthUser User { get; set; }
    }
}
