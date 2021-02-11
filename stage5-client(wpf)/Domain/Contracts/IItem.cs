using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IItem : IBaseRepo<ItemModel>
    {
        public IEnumerable<ItemModel> FindByFK(object id, string token);

        void CreateWitKey(ItemModel entity, string token, string key);
    }
}
