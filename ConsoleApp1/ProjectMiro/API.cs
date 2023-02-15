using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ConsoleApp1;
using ProjectMiro.Framework;
using RestSharp;

namespace ProjectMiro
{
    public class API
    {
        public static API Api { get; set; }
        public static void Enable()
        {
            Api = new API();
            RestClient client = new RestClient(BaseURL);
        }

        public static string BaseURL = "https://api.miro.com/v2/";
        public RestClient client;
        private string _bearerToken = "";
        public void MakeAPICall(string location, string json, Method method, ref string output)
        {
            var request = new RestRequest((string?) null, method);
            request.Resource = location;
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer: {_bearerToken}");
            request.AddParameter("application/json", json);
            var response = client.Execute(request);
            if (!response.IsSuccessful || response.StatusCode != HttpStatusCode.Accepted)
            {
                Log.Error($"Request invalid. (Status Code {response.StatusCode}), error message: {response.ErrorMessage}, exception: {response.ErrorException}.");
                output = "Error";
                return;
            }

            if(response.Content != null)
                output = response.Content;
            else
            {
                Log.Warn($"Response empty. Resource: {location}, Method: {method}");
            }
            return;

        }
    }

    
}
