using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.Idempotency
{
    public class IdempotentRequest
    {
        public string Key { get; set; }
        public Byte[] HashedRequest { get; set; }
        public string Response { get; set; }
        public string ExceptionType { get; set; }
        //public string 
    }
}
