using Domain.AggregatesModel.TaskListAggregate.cs;
using Infrastracture.DateTimeProvider;
using Infrastracture.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TodoAppAPI.LogHelper;

namespace TodoAppAPI.Application.Commands.TaskList
{
    public class DeleteTaskListCommandHandler : IRequestHandler<DeleteTaskListCommand, bool>
    {
        ITaskListRepository _taskListRepository;
        IDateTimeProvider _dateTimeProvider;
        ILogger<DeleteTaskListCommandHandler> _logger;
        public DeleteTaskListCommandHandler(ILogger<DeleteTaskListCommandHandler> logger, ITaskListRepository taskListRepository, IDateTimeProvider dateTimeProvider)
        {
           _taskListRepository = taskListRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteTaskListCommand command, CancellationToken cancellationToken)
        {
            var taskListToDelete = await _taskListRepository.GetAsync(command.Id);
            var batchToUpdateCopy = taskListToDelete.GetCopy() as TaskListAggregateModel;

            taskListToDelete.SoftDelete(_dateTimeProvider.UtcNow);

            _taskListRepository.Remove(taskListToDelete);

            var saveResult = await _taskListRepository.UnitOfWork.SaveEntitiesAsync();

            var logTemplate = new LogMessageTemplate();
            _logger.LogInformation(logTemplate.DeletedMessage(),
             command.GetType().Name, taskListToDelete, "Sample User", _dateTimeProvider.UtcNow, "Audit");

            return saveResult;
        }
    }
}
