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
    public class UpdateItemListCommandHandler : IRequestHandler<UpdateItemListCommand, bool>
    {
        private readonly IItemListRepository _itemListRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<UpdateItemListCommandHandler> _logger;
       
        public UpdateItemListCommandHandler(IItemListRepository itemListRepository,
         IDateTimeProvider dateTimeProvider, ILogger<UpdateItemListCommandHandler> logger)
        {
            _itemListRepository = itemListRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateItemListCommand command, CancellationToken cancellationToken)
        {
            ItemListAggregateModel taskListToUpdate = await _itemListRepository.GetAsync(command.Id);
            var accountToUpdateCopy = taskListToUpdate.GetCopy() as ItemListAggregateModel;


            if (taskListToUpdate == null)
            {
                throw new NotImplementedException();
            }

            taskListToUpdate.UpdateDetails(command.ItemName, command.ItemDetails, command.ItemStatus, _dateTimeProvider.UtcNow);

            var result = await _itemListRepository.UnitOfWork.SaveEntitiesAsync();

            var logTemplate = new LogMessageTemplate();
            _logger.LogInformation(logTemplate.UpdatedMessage(), command.GetType().Name,
                accountToUpdateCopy, taskListToUpdate, "Sample User", _dateTimeProvider.UtcNow, "Audit");

            return result;
        }


    }
}
