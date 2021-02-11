using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IBaseRepo<T>
    {
        IEnumerable<T> FindAll(string token);
        T FindById(int id, string token);
        //void FindById(object id);
        void Create(T entity, string token);
        void Update(T entity, string token);
        void Remove(T entity, string token);
    }
}
