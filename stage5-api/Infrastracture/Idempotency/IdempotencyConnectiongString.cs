using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.Idempotency
{
    public class IdempotencyConnectiongString
    {
        public IdempotencyConnectiongString(string value) => Value = value;
        public string Value { get; }
    }
}
