using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Masking.Serilog;
using Microsoft.Extensions.Configuration;
using Infrastracture.Repositories;
using TodoAppAPI.Controllers.Base;
using TodoAppAPI.Application.Queries.TaskList;
using Microsoft.AspNetCore.Authorization;
using TodoAppAPI.Application.Commands.TaskList;
using TodoAppAPI.Application.Commands.Idempotency;
using System.ComponentModel.DataAnnotations;
//using IdempotentAPI.Filters;

namespace TodoAppAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/tasklist")]
    [ApiController]
    public class TaskListController : BaseController
    {
        private readonly ITaskListQueries _taskListQueries;
        public TaskListController(ITaskListQueries taskListQueries) : base()
        {
            _taskListQueries = taskListQueries;
        }

        /// <summary>
        /// Get all TaskList
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTaskList()
        {
            var accounts = await _taskListQueries.GetAllTaskListAsync();

            return Ok(accounts);
        }

        /// <summary>
        /// Get specific Fee Scheme
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var feeSchemes = await _taskListQueries.GetTaskListAsyncById(id);

            return Ok(feeSchemes);
        }

        /// <summary>   
        /// Create new TaskList
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTaskList([FromBody] AddTaskListCommand command)
        {
            var result = await Mediator.Send(command);

            return CreatedAtAction("TaskList", null);
        }

        /// <summary>   
        /// Create new TaskList
        /// </summary>
        /// <returns></returns>
        [HttpPost("withkey")]
        [Authorize]
        public async Task<IActionResult> CreateTaskListWithKey([FromHeader(Name = "x-idempotency-key"), 
            Required] string idempotencyKey, [FromBody] AddTaskListDTO dto)
        {
            var command = new AddTaskListCommand();
            command.TaskName = dto.TaskName;
            command.TaskDetails = dto.TaskDetails;
            command.Email = dto.Email;

            var idempotenctCommand = new IdempotentCommand<AddTaskListCommand, AddTaskListResult>(command, idempotencyKey);

            var result = await Mediator.Send(idempotenctCommand);

            return CreatedAtAction("TaskList", result);
        }

        /// <summary>
        /// Update a specific TaskList
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTaskList([FromRoute] int id, [FromBody] UpdateTaskListCommand command)
        {
            command.Id = id;

            var result = await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Delete specific batch
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBatch([FromRoute] int id)
        {
            DeleteTaskListCommand command = new DeleteTaskListCommand
            {
                Id = id,
            };

            await Mediator.Send(command);

            return Ok();
        }

    }
}
/*
namespace TodoAppAPI.Controllers
{

    [Route("api/tasklist")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private ITaskListServices _services;

        public TaskListController(ITaskListServices services) => _services = services;

        [HttpGet]
        public IActionResult GetAll()
        {
            //serilog config / i use masking.serilog
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

            Log.Logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(configuration)
                 .Destructure.ByMaskingProperties(opts =>
                 {
                     opts.PropertyNames.Add("email");
                     opts.Mask = "******";
                 })
                 .CreateLogger();
            //end serilog config

            //get request
            var emp = _services.FindAll();
            return Ok(emp);
            //end get request
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var emp = _services.FindById(id);
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskListModel TaskLists)
        {
            try
            {
                Log.Information("Post Request on {@TaskList}", new TaskListModel
                {
                    idTask = TaskLists.idTask,
                    taskName = TaskLists.taskName,
                    taskDetails = TaskLists.taskDetails,
                    email = TaskLists.email
                }
                );

                _services.Create(TaskLists);
                return StatusCode(200, "Successfully Created!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskListModel TaskLists)
        {
            try
            {
                Log.Information("Update Request on {@TaskList}", new TaskListModel
                {
                    idTask = TaskLists.idTask,
                    taskName = TaskLists.taskName,
                    taskDetails = TaskLists.taskDetails,
                    email = TaskLists.email
                }
                );

                _services.Update(TaskLists);
                return StatusCode(200, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var emp = _services.FindById(id);
                _services.Remove(emp);
                return StatusCode(200, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

    }
}
*/