using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.DateTimeProvider
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow { get { return DateTime.UtcNow; } }
    }
}
    