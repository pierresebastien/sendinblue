using Newtonsoft.Json;

namespace Sendinblue.Api.Business
{
    public class Process
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "export_url")]
        public string Export_Url { get; set; }

    }

    public class ProcessResult
    {
        public Process[] Processes { get; set; }
        public int Count { get; set; }
    }
}
