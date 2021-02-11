using Domain.AggregatesModel.ItemListAggregate;
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
    public class AddItemListCommandHandler : IRequestHandler<AddItemListCommand, AddItemListResult>
    {
        private readonly IItemListRepository _itemListRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<AddItemListCommandHandler> _logger;



        public AddItemListCommandHandler(IItemListRepository itemListRepository,
            IDateTimeProvider dateTimeProvider, ILogger<AddItemListCommandHandler> logger)
        {
            _itemListRepository = itemListRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task<AddItemListResult> Handle(AddItemListCommand command, CancellationToken cancellationToken)
        {
        /*
            ItemListAggregateModel itemListToAdd = new ItemListAggregateModel(command.IdTask, command.ItemName, command.ItemDetails, command.ItemStatus, _dateTimeProvider.UtcNow);
            _itemListRepository.Add(itemListToAdd);

            var result = await _itemListRepository.UnitOfWork.SaveEntitiesAsync();

            var logTemplate = new LogMessageTemplate();

            _logger.LogInformation(logTemplate.CreatedMessage(),
                command.GetType().Name, itemListToAdd, "Sample User", _dateTimeProvider.UtcNow, "Audit");

            return result;
        */
            ItemListAggregateModel itemListToAdd = new ItemListAggregateModel(command.IdTask, command.ItemName, command.ItemDetails, command.ItemStatus, _dateTimeProvider.UtcNow);

            var result2 = new AddItemListResult(command.IdTask, command.ItemName, command.ItemDetails, command.ItemStatus);

            _itemListRepository.Add(itemListToAdd);

            var saveResult = await _itemListRepository.UnitOfWork.SaveEntitiesAsync();

            var logTemplate = new LogMessageTemplate();
            _logger.LogInformation(logTemplate.CreatedMessage(),
                command.GetType().Name, itemListToAdd, "Sample User", _dateTimeProvider.UtcNow, "Audit");

            return result2;
        }
    }
}
