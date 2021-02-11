using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Queries
{
    public class QueriesConnectionString
    {
        public QueriesConnectionString(string value) => Value = value;
        public string Value { get; }
    }
}
