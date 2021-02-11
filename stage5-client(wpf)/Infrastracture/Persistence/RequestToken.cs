using Domain.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace Infrastracture.Persistence
{
    public class RequestToken
    {
        //public UserModel userModel = new UserModel();


        RestClient restClient = new RestClient("https://dev.ppspepp.com/auth/realms/development/protocol/openid-connect/token");
        RestRequest request = new RestRequest();

        public string TokenRequest(UserModel userModel)
        {
            request = new RestRequest("", Method.POST);
            restClient.Authenticator = new HttpBasicAuthenticator(userModel.Username, userModel.Password);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("client_id", "disbursement");
            request.AddParameter("username", userModel.Username);
            request.AddParameter("password", userModel.Password);
            request.AddParameter("grant_type", "password");

            restClient.Execute(request);

            var responseJson = restClient.Execute(request).Content;
            var token = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson)["access_token"].ToString();
            if (token.Length == 0)
            {
                throw new AuthenticationException("API authentication failed.");
            }

            return token;
        }
    }
}
