using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoAppAPI.Extension;

namespace TodoAppAPI.Application.Behaviors
{       
    /*
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        private readonly ToDoAppDbContext _dbContext;

        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IIntegrationEvent _integrationEvent;
        public TransactionBehaviour(ToDoAppDbContext dbContext, ILogger<TransactionBehaviour<TRequest, TResponse>> logger, IIntegrationEvent integrationEvent)
        {
            _dbContext = dbContext;
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
            _integrationEvent = integrationEvent;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    {
                        _logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);
                    }

                });

                await _integrationEvent.Send();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }   
    */
}
