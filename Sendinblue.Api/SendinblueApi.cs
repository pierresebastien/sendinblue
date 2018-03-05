using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using Sendinblue.Api.Business;

namespace Sendinblue.Api
{
    public class SendinblueAPI
    {
        const string URL = "https://api.sendinblue.com/v3/";
        public const string Ip = "ip";
        public const string Domain = "domain";

        public string ApiKey { get; set; }
        public int ApiTimeout { get; set; }

        RestClient _client;



        public SendinblueAPI(string apiKey, int apiTimeout = 2000)
        {
            ApiKey = apiKey;
            ApiTimeout = apiTimeout;
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new ArgumentException("Apikey is empty.");
            }
            _client = new RestClient(URL);
        }

        #region Account

        public Account GetAccount()
        {
            var request = new RestRequest("account")
            {
                Method = Method.GET,
                Timeout = ApiTimeout
            };
            request.AddHeader("api-key", ApiKey);

            IRestResponse response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebException("Invalid request account.");
            }
            try
            {
                var account = JsonConvert.DeserializeObject<Account>(response.Content);
                return account;
            }
            catch (Exception e)
            {
                throw new Exception($"Invalid deserialize object: {e.Message}.");
            }
        }

        #endregion

        #region Sender

        public IEnumerable<Sender> GetSenders(Dictionary<string,string> datas = null)
        {
            var request = new RestRequest("senders")
            {
                Method = Method.GET,
                Timeout = ApiTimeout
            };

            request.AddHeader("api-key", ApiKey);

            if (datas.ContainsKey(Ip))
            {
                request.AddQueryParameter(Ip, datas[Ip]);
            }

            if (datas.ContainsKey(Domain))
            {
                request.AddQueryParameter(Domain, datas[Domain]);
            }

            IRestResponse response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new WebException("Invalid request senders.");
            }
            try
            {
                var senderResult = JsonConvert.DeserializeObject<SenderResult>(response.Content);
                return senderResult.Senders;
            }
            catch (Exception e)
            {
                throw new Exception($"Invalid deserialize object: {e.Message}");
            }
        }

        public void CreateSender(string name, string email, IpSender [] ips = null)
        {
            if(string.IsNullOrEmpty(name)){
                throw new IllegalArgumentException("Name is empty.");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new IllegalArgumentException("Email is empty.");
            }

            var request = new RestRequest("senders")
            {
                Method = Method.POST,
                Timeout = ApiTimeout
            };

            request.AddHeader("api-key", ApiKey);

            request.AddParameter("name",name);
            request.AddParameter("email", email);

            if(ips != null){
                var json = JsonConvert.SerializeObject(ips);
                request.AddParameter("ips",json);
            }

            IRestResponse response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new WebException("Invalid request create sender.");
            }

        }

        public void UpdateSender(string senderId, string name, string email, IpSender[] ips = null)
        {
            if(string.IsNullOrEmpty(senderId)){
                throw new IllegalArgumentException("SenderId is empty.")
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new IllegalArgumentException("Name is empty.");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new IllegalArgumentException("Email is empty.");
            }

            var request = new RestRequest($"senders/{senderId}")
            {
                Method = Method.POST,
                Timeout = ApiTimeout
            };

            request.AddHeader("api-key", ApiKey);

            request.AddParameter("name", name);
            request.AddParameter("email", email);

            if (ips != null)
            {
                var json = JsonConvert.SerializeObject(ips);
                request.AddParameter("ips", json);
            }

            IRestResponse response = _client.Execute(request);

            if(response.StatusCode == HttpStatusCode.NotFound){
                throw new WebException($"SenderIp :{senderId} is not found.");
            }

            if(response.StatusCode == HttpStatusCode.BadRequest){
                throw new WebException($"Bad request : {response.ErrorMessage}");
            }

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new WebException("Invalid request account.");
            }

        }

        #endregion
    }
}
