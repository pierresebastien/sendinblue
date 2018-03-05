using System;
using Sendinblue.Api;
using Xunit;

namespace Sendinblue.Test
{
    public class SendinblueInstance
    {
        public const string APIKEY = "Your API Key";

        [Fact]
        public void CreateInstance()
        {
            var apiKey = "apikey";
            var sendinblueApi = new SendinblueAPI(apiKey);
            Assert.NotNull(sendinblueApi);
            Assert.Equal(apiKey, sendinblueApi.ApiKey);
        }

        [Fact]
        public void CreateInstanceWithTimeOut()
        {
            var apiKey = "apikey";
            var apiTimeout = 3000;
            var sendinblueApi = new SendinblueAPI(apiKey, apiTimeout);
            Assert.NotNull(sendinblueApi);
            Assert.Equal(apiKey, sendinblueApi.ApiKey);
            Assert.Equal(apiTimeout, sendinblueApi.ApiTimeout);
        }

        [Fact]
        public void CreateInstanceFailed()
        {
            Assert.Throws<ArgumentException>(() => new SendinblueAPI(""));
        }

        [Fact]
        public void GetAccount()
        {
            var sendinblueApi = new SendinblueAPI(APIKEY);
            var account = sendinblueApi.GetAccount();
            Assert.NotNull(account);
        }

        [Fact]
        public void GetSenders()
        {
            var sendinblueApi = new SendinblueAPI(APIKEY);
            var senders = sendinblueApi.GetSenders();
            Assert.NotNull(senders);
        }
    }
}
