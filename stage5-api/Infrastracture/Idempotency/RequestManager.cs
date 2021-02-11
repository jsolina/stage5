using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Dapper;

namespace Infrastracture.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private IDbConnection _connection;
        public RequestManager(IDbConnection connection) 
        {
            _connection = connection;
        }
        public async Task<IdempotentRequest> GetAsync(string key)
        {
            string query = "SELECT `key`,request as 'HashedRequest',response,exception_type as 'ExceptionType' FROM idempotent_request WHERE `key` = @key";

            var idempotentRequest = await _connection.QueryAsync<IdempotentRequest>(query, new { key });

            return idempotentRequest.FirstOrDefault();
        }
        public async Task CreateRequestForCommandAsync<T>(string key, byte[] hashedRequest, string response, string exceptionType = default(string))
        {
            string query = "INSERT INTO idempotent_request (`key`,request,response,exception_type) VALUES (@key,@hashedRequest,@response,@exceptionType)";

            await _connection.ExecuteAsync(query, new { key, hashedRequest, response, exceptionType });
        }
    }
}
