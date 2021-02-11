﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Seedwork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
