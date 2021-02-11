using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Queries.ItemList
{
    public interface IItemListQueries
    {
        Task<IList<ItemLstViewModel>> GetAllItemListAsync();
        Task<IList<ItemLstViewModel>> GetAllItemListAsyncByIdTask(int id_task);

        Task<ItemLstViewModel> GetItemListAsyncById(int id);

        //Task<ItemLstViewModel> GetItemListAsyncById(int id);
    }
}
