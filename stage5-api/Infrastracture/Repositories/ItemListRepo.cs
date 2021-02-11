using Domain.AggregatesModel.ItemListAggregate;
using Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories
{
    public class ItemListRepo : IItemListRepository
    {
        private readonly ToDoAppDbContext _context;

        public ItemListRepo(ToDoAppDbContext context)
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

        public ItemListAggregateModel Add(ItemListAggregateModel account)
        {
            return _context.ItemList.Add(account).Entity;
        }

        public async Task<ItemListAggregateModel> GetAsync(int idTask)
        {
            var account = await _context.ItemList.FirstOrDefaultAsync(a => a.Id == idTask);

            return account;
        }

        public void Update(ItemListAggregateModel account)
        {
            _context.Entry(account).State = EntityState.Modified;
        }

        public void Remove(ItemListAggregateModel entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
    }
}
