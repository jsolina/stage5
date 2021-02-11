using Domain.AggregatesModel.TaskListAggregate.cs;
using Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories
{
    public class TaskListRepo : ITaskListRepository
    {
        private readonly ToDoAppDbContext _context;

        public TaskListRepo(ToDoAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
                
        public TaskListAggregateModel Add(TaskListAggregateModel entity)
        {
            return _context.TaskList.Add(entity).Entity;
        }

        public async Task<TaskListAggregateModel> GetAsync(int idTask)
        {
              var account = await _context.TaskList.FirstOrDefaultAsync(a => a.Id == idTask);

            return account;
        }

        public void Update(TaskListAggregateModel entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TaskListAggregateModel entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

    }
    /*
    public class TaskListRepo : ITaskListRepo
    {
        private IDatabaseContext _dbcontext;

        public TaskListRepo(IDatabaseContext context) => _dbcontext = context;

        public void aw()
        {
            throw new NotImplementedException();
        }

        public void Create(TaskListModel entity)
        {
            //entity.idTaskList = Guid.NewGuid();
            _dbcontext.TaskList.Add(entity);
            _dbcontext.Save();
        }

        public IEnumerable<TaskListModel> FindAll()
        {
            return _dbcontext.TaskList.ToList().OrderBy(c => c.taskName);
        }

        public TaskListModel FindById(int id)
        {
            return _dbcontext.TaskList.Where(d => d.idTask.Equals(id)).FirstOrDefault();
        }

        public void Remove(TaskListModel entity)
        {
            _dbcontext.TaskList.Remove(entity);
            _dbcontext.Save();
        }

        public void Update(TaskListModel entity)
        {
            _dbcontext.TaskList.Update(entity);
            _dbcontext.Save();
        }
    }
    */
}
    