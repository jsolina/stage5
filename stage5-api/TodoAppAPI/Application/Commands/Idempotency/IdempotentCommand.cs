using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Commands.Idempotency
{
    public class IdempotentCommand<T, R> : IRequest<R> where T : IRequest<R>
    {
        public T Command { get; }
        public string Key { get; }
        public IdempotentCommand(T command, string key)
        {
            Command = command;
            Key = key;
        }
    }
}
