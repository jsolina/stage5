using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
