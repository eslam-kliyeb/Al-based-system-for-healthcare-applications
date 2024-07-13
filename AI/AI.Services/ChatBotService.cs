using AI.Core.Interfaces.Service;
using AI.Repository.ApiHelper;
using Newtonsoft.Json;
using System.Text;

namespace AI.Services
{
    public class ChatBotService : IChatBotService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> ChatBot(string question)
        {
            string? answer = "null";
            string apiUrl = "https://8000-01j0gz6mrnynb447xzngf5sqhj.cloudspaces.litng.ai/query";
            var testQuery = new
            {
                query = $"{question}"
            };
            string jsonQuery = JsonConvert.SerializeObject(testQuery);
            var content = new StringContent(jsonQuery, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData>(responseBody);
            answer = responseData.GeneratedResponse;
            return answer;
        }
    }
}
