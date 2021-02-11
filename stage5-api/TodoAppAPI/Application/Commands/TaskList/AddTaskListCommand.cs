using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    
namespace TodoAppAPI.Application.Commands.TaskList
{
    [Serializable]
    public class AddTaskListCommand : BaseCommand, IRequest<AddTaskListResult>
    {
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public string Email { get; set; }
    }

    public class AddTaskListDTO
    {
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public string Email { get; set; }
    }

    public class AddTaskListResult
    {
        public AddTaskListResult(string taskName, string taskDetails, string email)
        {
           // Id = id;
            TaskName = taskName;
            TaskDetails = taskDetails;
            Email = email;
        }

        //public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public string Email { get; set; }
    }


}
