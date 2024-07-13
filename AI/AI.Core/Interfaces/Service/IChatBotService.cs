
namespace AI.Core.Interfaces.Service
{
    public interface IChatBotService
    {
        Task<string> ChatBot(string question);
    }
}
