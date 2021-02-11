using Domain.AggregatesModel.ItemListAggregate;
using Domain.AggregatesModel.TaskListAggregate.cs;
using Infrastracture.DateTimeProvider;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoAppAPI.LogHelper;

namespace TodoAppAPI.Application.Commands.ItemList
{
    public class DeleteItemListCommandHandler : IRequestHandler<DeleteItemListCommand, bool>
    {
        IItemListRepository _itemListRepository;
        IDateTimeProvider _dateTimeProvider;
        ILogger<DeleteItemListCommandHandler> _logger;
        public DeleteItemListCommandHandler(ILogger<DeleteItemListCommandHandler> logger, IItemListRepository itemListRepository, IDateTimeProvider dateTimeProvider)
        {
            _itemListRepository = itemListRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteItemListCommand command, CancellationToken cancellationToken)
        {
            var itemListToDelete = await _itemListRepository.GetAsync(command.Id);

            itemListToDelete.SoftDelete(_dateTimeProvider.UtcNow);

            _itemListRepository.Remove(itemListToDelete);

            var saveResult = await _itemListRepository.UnitOfWork.SaveEntitiesAsync();

            var logTemplate = new LogMessageTemplate();
            _logger.LogInformation(logTemplate.DeletedMessage(),
             command.GetType().Name, itemListToDelete, "Sample User", _dateTimeProvider.UtcNow, "Audit");

            return saveResult;
        }

    }
}
