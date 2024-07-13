using AI.Core.Interfaces.Service;
using StackExchange.Redis;
using System.Text.Json;

namespace AI.Services
{
    public class CashService : ICashService
    {
        private readonly IDatabase _database;
        public CashService(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetCashResponseAsync(string key)
        {
            var response = await _database.StringGetAsync(key);
            return response.IsNullOrEmpty ? null : response.ToString();
        }
        public async Task SetCashResponseAsync(string key, object response, TimeSpan time)
        {
            var serializedResponse = JsonSerializer.Serialize(response,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await _database.StringSetAsync(key, serializedResponse, time);
        }
    }
}
