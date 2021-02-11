using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Seedwork
{
    public abstract class Entity<T> : EntityEvents
    {
        T _Id;
        public virtual T Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        public DateTime CreatedAt { get; protected set; }
        public DateTime LastModified { get; protected set; }

        //public bool Deleted { get; protected set; }

        public object GetCopy()
        {
            return this.MemberwiseClone();
        }
    }   
}
