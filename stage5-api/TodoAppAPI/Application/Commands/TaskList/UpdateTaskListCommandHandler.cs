using Domain.AggregatesModel.TaskListAggregate.cs;
using Infrastracture.DateTimeProvider;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoAppAPI.Application.Commands.TaskList;
using TodoAppAPI.LogHelper;

namespace TodoAppAPI.Application.Commands
{
    public class UpdateTaskListCommandHandler : IRequestHandler<UpdateTaskListCommand, bool>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<UpdateTaskListCommandHandler> _logger;
        public UpdateTaskListCommandHandler(ITaskListRepository taskListRepository, 
            IDateTimeProvider dateTimeProvider, ILogger<UpdateTaskListCommandHandler> logger)
        {
            _taskListRepository = taskListRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateTaskListCommand command, CancellationToken cancellationToken)
        {
            TaskListAggregateModel taskListToUpdate = await _taskListRepository.GetAsync(command.Id);
            var accountToUpdateCopy = taskListToUpdate.GetCopy() as TaskListAggregateModel;


            if (taskListToUpdate == null)
            {
                throw new NotImplementedException();
            }

            taskListToUpdate.UpdateDetails(command.TaskName, command.TaskDetails, command.Email, _dateTimeProvider.UtcNow);

            var result = await _taskListRepository.UnitOfWork.SaveEntitiesAsync();

            var logTemplate = new LogMessageTemplate();
            _logger.LogInformation(logTemplate.UpdatedMessage(),command.GetType().Name, 
                accountToUpdateCopy, taskListToUpdate, "Sample User", _dateTimeProvider.UtcNow, "Audit");

            return result;
        }
    }
}
