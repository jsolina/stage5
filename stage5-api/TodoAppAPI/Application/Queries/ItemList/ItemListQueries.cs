using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TodoAppAPI.Application.Queries.QueriesTypeHandlers;
using TodoAppAPI.Application.Queries.TaskList;

namespace TodoAppAPI.Application.Queries.ItemList
{
    public class ItemListQueries : IItemListQueries
    {
        private IDbConnection _connection;
        public ItemListQueries(IDbConnection connection)
        {
            _connection = connection;

            SqlMapper.AddTypeHandler(new StringCollectionTypeHandler());
        }

        public async Task<IList<ItemLstViewModel>> GetAllItemListAsync()
        {
            string query = "SELECT id AS 'Id', id_task AS 'IdTask', item_name AS 'ItemName', item_details AS 'ItemDetails', item_status AS 'ItemStatus', created_at AS 'CreatedAt', last_modified AS 'LastModified' FROM itemlist";

            var result = await _connection.QueryAsync<ItemLstViewModel>(query);

            return result.ToList();
        }

        public async Task<IList<ItemLstViewModel>> GetAllItemListAsyncByIdTask(int id_task)
        {
            string query = "SELECT id AS 'Id', id_task AS 'IdTask', item_name AS 'ItemName', item_details AS 'ItemDetails', item_status AS 'ItemStatus', created_at AS 'CreatedAt', last_modified AS 'LastModified' FROM itemlist WHERE id_task = @id_task";
            var result = await _connection.QueryAsync<ItemLstViewModel>(query, new { IdTask = id_task });

            return result.ToList();
        }
        
        public async Task<ItemLstViewModel> GetItemListAsyncById(int id)
        {
            string query = "SELECT id AS 'Id', id_task AS 'IdTask', item_name AS 'ItemName', item_details AS 'ItemDetails', item_status AS 'ItemStatus', created_at AS 'CreatedAt', last_modified AS 'LastModified' FROM itemlist WHERE id = @Id";
            var result = (await _connection.QueryAsync<ItemLstViewModel>(query, new { Id = id })).FirstOrDefault();

            return result;
        }
    }
}
