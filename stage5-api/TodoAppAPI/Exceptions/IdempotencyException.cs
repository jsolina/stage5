using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Exceptions
{
    public class IdempotencyException : Exception
    {
        public IdempotencyException() : base() { }
    }
}
