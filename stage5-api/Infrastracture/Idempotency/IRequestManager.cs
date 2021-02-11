using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Idempotency
{
    public interface IRequestManager
    {
        //Task<bool> ExistAsync(string key,byte[] hashedRequest);
        Task<IdempotentRequest> GetAsync(string key);
        Task CreateRequestForCommandAsync<T>(string ky, byte[] hashedRequest, string response, string exceptionType = default(string));
    }
}
