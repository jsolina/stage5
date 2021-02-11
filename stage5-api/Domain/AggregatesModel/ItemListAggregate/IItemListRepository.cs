using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregatesModel.ItemListAggregate
{
    public interface IItemListRepository : IRepository<ItemListAggregateModel>
    {
        ItemListAggregateModel Add(ItemListAggregateModel account);

        void Update(ItemListAggregateModel updatedAccount);

        void Remove(ItemListAggregateModel entity);

        Task<ItemListAggregateModel> GetAsync(int idTask);
    }
}
