using Infrastracture.Idempotency;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoAppAPI.Exceptions;
using TodoAppAPI.Extension;

namespace TodoAppAPI.Application.Commands.Idempotency
{   
    public class IdempotentCommandHandler<T, R> : IRequestHandler<IdempotentCommand<T, R>, R>
        where T : IRequest<R>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;
        public IdempotentCommandHandler(IRequestManager requestManager, IMediator mediator)
        {
            _mediator = mediator;
            _requestManager = requestManager;
        }
        public async Task<R> Handle(IdempotentCommand<T, R> message, CancellationToken cancellationToken)
        {
            byte[] hashedCommand = JsonConvert.SerializeObject(message.Command).SHA1Hash();

            var existingIdempotentRequest = await _requestManager.GetAsync(message.Key);

            //Process the request if there is no existing idempotent request
            if (existingIdempotentRequest == null)
            {
                R result = default(R);
                string serializedResult = default(string);
                try
                {
                    result = await _mediator.Send(message.Command);

                    serializedResult = JsonConvert.SerializeObject(result);

                    //Serialize the result and save it  
                    await _requestManager.CreateRequestForCommandAsync<T>(message.Key, hashedCommand, serializedResult);
                }
                catch (Exception ex)
                {
                    //If there is an error occurs inside  the handler serialize the exception and save it 
                    serializedResult = JsonConvert.SerializeObject(ex);

                    await _requestManager.CreateRequestForCommandAsync<T>(message.Key, hashedCommand, serializedResult, ex.GetType().ToString());

                    throw;
                }

                return result;
            }
                
            //Throw idempotency error if request payload doesnt match to saved request payload


            if (!hashedCommand.SequenceEqual(existingIdempotentRequest.HashedRequest))
            {
                //throw new IdempotencyException();
            }

            //The request is duplicate return the response of the first request
            return CreateResultForDuplicateRequest(existingIdempotentRequest.Response, existingIdempotentRequest.ExceptionType);
        }

        /// <summary>
        /// Creates the result value to return if a previous request was found
        /// </summary>
        /// <returns></returns>
        protected virtual R CreateResultForDuplicateRequest(string savedResult, string exceptionType)
        {
            R result = default(R);

            if (string.IsNullOrEmpty(exceptionType))
            {
                result = JsonConvert.DeserializeObject<R>(savedResult);
            }
            else
            {
                var exception = (Exception)JsonConvert.DeserializeObject(savedResult, Type.GetType(exceptionType));
                throw exception;
            }

            return result;
        }

        protected virtual R CreateResultForDuplicateRequest()
        {
            return default(R);
        }
    }
}
