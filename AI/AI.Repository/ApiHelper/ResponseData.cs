using Newtonsoft.Json;

namespace AI.Repository.ApiHelper
{
    public class ResponseData
    {
        [JsonProperty("generated_response")]
        public string GeneratedResponse { get; set; }
    }
}
