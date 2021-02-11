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

namespace TodoAppAPI.Application.Commands.TaskList
{
    public class AddTaskListCommandHandler : IRequestHandler<AddTaskListCommand, AddTaskListResult>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<AddTaskListCommandHandler> _logger;

        public AddTaskListCommandHandler(ITaskListRepository taskListRepository,
            IDateTimeProvider dateTimeProvider, ILogger<AddTaskListCommandHandler> logger)
        {
            _taskListRepository = taskListRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task<AddTaskListResult> Handle(AddTaskListCommand command, CancellationToken cancellationToken)
        {

            TaskListAggregateModel taskListToAdd = new TaskListAggregateModel(command.TaskName, command.TaskDetails, command.Email, _dateTimeProvider.UtcNow);

            var result2 = new AddTaskListResult(command.TaskName, command.TaskDetails, command.Email);

            _taskListRepository.Add(taskListToAdd);

            var saveResult = await _taskListRepository.UnitOfWork.SaveEntitiesAsync();

            var logTemplate = new LogMessageTemplate();
            _logger.LogInformation(logTemplate.CreatedMessage(),
                command.GetType().Name, taskListToAdd, "Sample User", _dateTimeProvider.UtcNow, "Audit");

            return result2;
        }
    }
    /*
    public class AddTaskListCommandHandler : IRequestHandler<AddTaskListCommand, AddTaskListResult>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<AddTaskListCommandHandler> _logger;

        public AddTaskListCommandHandler(ITaskListRepository taskListRepository,
            IDateTimeProvider dateTimeProvider, ILogger<AddTaskListCommandHandler> logger)
        {
            _taskListRepository = taskListRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task<AddTaskListResult> Handle(AddTaskListCommand command, CancellationToken cancellationToken)
        {

            TaskListAggregateModel categoryToAdd = new TaskListAggregateModel(command.TaskName, command.TaskDetails, command.Email, _dateTimeProvider.UtcNow);

            _taskListRepository.Add(categoryToAdd);

            var result = await _taskListRepository.UnitOfWork.SaveEntitiesAsync();



            var logTemplate = new LogMessageTemplate();

            _logger.LogInformation(logTemplate.CreatedMessage(),
                command.GetType().Name, categoryToAdd, "Sample User", _dateTimeProvider.UtcNow, "Audit");

            return result;
        }
    }
    */

}
