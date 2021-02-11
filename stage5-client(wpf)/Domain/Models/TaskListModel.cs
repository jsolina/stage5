using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class TaskListModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public string LastModified { get; set; }
    }
}