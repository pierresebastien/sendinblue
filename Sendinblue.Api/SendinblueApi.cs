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
        public const string Limit = "limit";
        public const string Offset = "offset";
        public const string Name = "name";
        public const string Email = "email";
        public const string Ips = "ips";

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
            return CallApi<Account>(Method.GET, "account");
        }

        #endregion

        #region Sender

        public IEnumerable<Sender> GetSenders(string ip = null, string domain = null)
        {

            var datas = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(ip))
            {
                datas.Add(Ip, ip);
            }

            if (!string.IsNullOrEmpty(domain))
            {
                datas.Add(Domain, domain);
            }

            var senderResult = CallApi<SenderResult>(Method.GET, "senders", datas);

            return senderResult.Senders;
        }

        public CreateSenderResult CreateSender(string name, string email, IpSender[] ips = null)
        {
            var parameters = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name is empty.");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is empty.");
            }

            parameters.Add(Name, name);
            parameters.Add(Email, email);

            if (ips != null)
            {
                var json = JsonConvert.SerializeObject(ips);
                parameters.Add(Ips, json);
            }

            return CallApi<CreateSenderResult>(Method.POST, "senders", parameters: parameters);
        }

        public void UpdateSender(int senderId, string name, string email, IpSender[] ips = null)
        {
            var parameters = new Dictionary<string, string>();

            if (senderId != 0)
            {
                throw new ArgumentException("SenderId is empty.");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name is empty.");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is empty.");
            }

            parameters.Add(Name, name);
            parameters.Add(Email, email);

            if (ips != null)
            {
                var json = JsonConvert.SerializeObject(ips);
                parameters.Add(Ips, json);
            }

            var result = CallApi<int>(Method.PUT, $"senders/{senderId}", parameters: parameters, typeOfT: "senderId");
            if (result != 0)
            {
                throw new Exception("Update sender failed.");
            }
        }

        public void DeleteSender(int senderId)
        {
            if (senderId != 0)
            {
                throw new ArgumentException("SenderId is empty.");
            }

            var result = CallApi<int>(Method.DELETE, $"senders/{senderId}", typeOfT: "senderId");

            if (result != 0)
            {
                throw new Exception("Update sender failed.");
            }
        }

        public IEnumerable<IpSender> GetIpsById(int senderId)
        {

            if (senderId != 0)
            {
                throw new ArgumentException("SenderId is empty.");
            }

            var ipsResult = CallApi<IpsResult>(Method.GET, $"senders/{senderId}/ips", typeOfT: "senderIp");

            return ipsResult.Ips;

        }

        public IEnumerable<IpSender> GetIps()
        {
            var ipsResult = CallApi<IpsResult>(Method.GET, $"senders/ips");
            return ipsResult.Ips;
        }

        #endregion

        #region Process

        public ProcessResult GetProcess(int limit = 0, int offset = 0)
        {
            var parameters = new Dictionary<string, string>();

            if (limit != 0)
            {
                parameters.Add(Limit, limit.ToString());
            }

            if (offset != 0)
            {
                parameters.Add(Offset, offset.ToString());
            }

            return CallApi<ProcessResult>(Method.GET, "processes", parameters: parameters);
        }

        public Process GetProcessById(int processId, int limit = 0, int offset = 0)
        {

            if (processId != 0)
            {
                throw new ArgumentException("Proccess Id is empty.");
            }

            var parameters = new Dictionary<string, string>();

            if (limit != 0)
            {
                parameters.Add(Limit, limit.ToString());
            }

            if (offset != 0)
            {
                parameters.Add(Offset, offset.ToString());
            }

            return CallApi<Process>(Method.GET, $"processes/{processId}", parameters: parameters, typeOfT: "ProcessId");
        }

        #endregion

        #region Campaign
        public IEnumerable<Campaign> GetCampaigns()
        {
            var campaignResult = CallApi<CampaignResult>(Method.GET, "emailCampaigns");
            return campaignResult.Campaigns;
        }

        public Campaign GetCampaign(int campaignId)
        {
            if (campaignId == 0)
            {
                throw new ArgumentException("CampaignId is empty.");
            }

            return CallApi<Campaign>(Method.GET, $"emailCampaigns/{campaignId}", typeOfT: "CampaignId");
        }

        #endregion

        #region Contact

        public IEnumerable<Contact> GetContacts()
        {
            var contactResult = CallApi<ContactsResult>(Method.GET, "contacts");
            return contactResult.Contacts;
        }

        public Contact GetContactByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is empty.");
            }
            return CallApi<Contact>(Method.GET, $"contacts/{email}", typeOfT: "email");
        }

        #endregion

        private T CallApi<T>(Method method, string request,
                             Dictionary<string, string> data = null,
                             Dictionary<string, string> parameters = null,
                             string typeOfT = null)
        {
            var restRequest = new RestRequest(request)
            {
                Method = method,
                Timeout = ApiTimeout
            };

            restRequest.AddHeader("api-key", ApiKey);

            if (data != null)
            {
                foreach (var key in data.Keys)
                {
                    restRequest.AddQueryParameter(key, data[key]);
                }
            }

            if (parameters != null)
            {
                foreach (var key in parameters.Keys)
                {
                    restRequest.AddParameter(key, parameters[key]);
                }
            }

            var response = _client.Execute(restRequest);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new WebException($"{typeOfT} is not found.");
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var badRequest = JsonConvert.DeserializeObject<BadRequest>(response.Content);
                throw new WebException($"Bad request: {badRequest.Message}.");
            }

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return default(T);
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return default(T);
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception e)
            {
                throw new Exception($"Invalid deserialize object: {e.Message}");
            }
        }
    }
}
