using Microsoft.AspNetCore.Http;

namespace AI.Core.Interfaces.Service
{
    public interface IClassifierService
    {
        Task<string> Classifier(IFormFile Image);
    }
}
