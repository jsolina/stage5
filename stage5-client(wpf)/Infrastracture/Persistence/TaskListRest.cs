using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Domain.Models;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Security.Authentication;

namespace Infrastracture.Persistence
{
    public class TaskListRest: RequestToken
    {
        Client Clients = new Client();
        RestClient restClient = new RestClient();
        //RestClient restClient2 = new RestClient("https://dev.ppspepp.com/auth/realms/development/protocol/openid-connect/token");

        RestRequest request = new RestRequest();

        public string _request = "api/tasklist";
        string apiVersion = "?api-version=1.0";

        public TaskListModel TaskLists { get; set; }    

        public TaskListRest()
        {
            restClient = new RestClient(Clients.client);
        }
        public IEnumerable<TaskListModel> GetRequest()
        {
            request = new RestRequest(_request, Method.GET);
            var queryResult = restClient.Execute<List<TaskListModel>>(request).Data;
            return queryResult;
        }

        public IEnumerable<TaskListModel> GetRequestWithToken(string _token)
        {
            request = new RestRequest(_request + apiVersion, Method.GET);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, "Bearer");
            var queryResult = restClient.Execute<List<TaskListModel>>(request).Data;
            return queryResult;
        }


        public TaskListModel GetByIdRequest(int entity, string _token)
        {
            request = new RestRequest(_request + "/{Id}" + apiVersion, Method.GET);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, "Bearer");
            request.AddUrlSegment("Id", entity);

            var queryResult = restClient.Execute<TaskListModel>(request).Data;
            return queryResult;
        }

        public void PostRequest(TaskListModel entity, string _token)
        {
            request = new RestRequest(_request + apiVersion, Method.POST);
            restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", _token));
            request.AddHeader("x-idempotency-key", Guid.NewGuid().ToString()); //idempotency key header
            request.AddHeader("charset", "utf-8 ");
            MakeRequest(entity);
        }
        public int PostRequestWithKey(TaskListModel entity, string _token, string key)
        {
            request = new RestRequest(_request + "/withkey" + apiVersion, Method.POST);
            restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", _token));
            request.AddHeader("x-idempotency-key", key); //idempotency key header   
            request.AddHeader("charset", "utf-8 ");
            return MakeRequest(entity);
        }

        public void PutRequest(TaskListModel entity, string _token)
        {
            request = new RestRequest(_request + "/" + entity.Id + apiVersion, Method.PUT);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, "Bearer");
            MakeRequest(entity);
        }

        public void DeleteRequest(TaskListModel entity, string _token)
        {
            request = new RestRequest(_request + "/{Id}" + apiVersion, Method.DELETE);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, "Bearer");
            request.AddUrlSegment("Id", entity.Id);
            restClient.Execute<TaskListModel>(request);
        }

        public int MakeRequest(TaskListModel entity)
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(entity);
            restClient.Execute(request);


            RestResponse response = (RestResponse)restClient.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
            return numericStatusCode;
        }
    }
}
