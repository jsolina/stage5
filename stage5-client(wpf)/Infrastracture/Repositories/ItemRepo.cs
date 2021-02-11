using Domain.Contracts;
using Domain.Models;
using Infrastracture.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastracture.Repositories
{
    public class ItemRepo : IItem
    {
        ItemRest _dbcontext = new ItemRest();

        public void Create(ItemModel entity, string token)
        {
            _dbcontext.PostRequest(entity, token);
        }

        public void CreateWitKey(ItemModel entity, string token, string key)
        {
            _dbcontext.PostRequestWithKey(entity, token, key);
        }

        public IEnumerable<ItemModel> FindAll(string token)
        {
            return _dbcontext.GetRequest(token);
        }

        public IEnumerable<ItemModel> FindByFK(object id, string token)
        {
            return _dbcontext.GetRequest(token);
        }

        public ItemModel FindById(int id, string token)
        {
            return _dbcontext.GetByIdRequest(id, token);
        }

        public void Remove(ItemModel entity, string token)
        {
            _dbcontext.DeleteRequest(entity, token);
        }


        public void Update(ItemModel entity, string token)
        {
            _dbcontext.PutRequest(entity, token);
        }
    }
}
