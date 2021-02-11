using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Domain.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Infrastracture.Persistence
{
    public class ItemRest
    {
        Client Clients = new Client();
        RestClient restClient = new RestClient();
        RestRequest request = new RestRequest();

        public string _request = "api/itemlist";
        string apiVersion = "?api-version=1.0";

        public ItemModel Items { get; set; }

        public ItemRest()
        {
            restClient = new RestClient(Clients.client);
        }

        public IEnumerable<ItemModel> GetRequest(string _token)
        {
            request = new RestRequest(_request + apiVersion, Method.GET);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, "Bearer");
            var queryResult = restClient.Execute<List<ItemModel>>(request).Data;
            return queryResult;
        }

        public ItemModel GetByIdRequest(int entity, string _token)
        {
            request = new RestRequest(_request + "/{Id}" + apiVersion, Method.GET);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, "Bearer");
            request.AddUrlSegment("Id", entity);

            var queryResult = restClient.Execute<ItemModel>(request).Data;
            return queryResult;
        }

        public void PostRequest(ItemModel entity, string _token)
        {
            request = new RestRequest(_request + apiVersion, Method.POST);
            restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", _token));
            request.AddHeader("charset", "utf-8 ");
            MakeRequest(entity);
        }

        public void PostRequestWithKey(ItemModel entity, string _token, string key)
        {
            request = new RestRequest(_request + "/withkey" + apiVersion, Method.POST);
            restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", _token));
            request.AddHeader("x-idempotency-key", key); //idempotency key header
            request.AddHeader("charset", "utf-8 ");
            MakeRequest(entity);
        }

        public void PutRequest(ItemModel entity, string _token)
        {

            request = new RestRequest(_request + "/" + entity.Id + apiVersion, Method.PUT);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, "Bearer");

            MakeRequest(entity);
        }

        public void DeleteRequest(ItemModel entity, string _token)
        {
            request = new RestRequest(_request + "/{Id}" + apiVersion, Method.DELETE);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, "Bearer");
            request.AddUrlSegment("Id", entity.Id);
            restClient.Execute<ItemModel>(request);
        }

        public void MakeRequest(ItemModel entity)
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(entity);
            restClient.Execute(request);
        }

    }
}

