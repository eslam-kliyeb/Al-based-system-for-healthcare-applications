using Microsoft.AspNetCore.Http;

namespace AI.Core.Interfaces.Service
{
    public interface ISegmentationService
    {
        Task<string> Segmentation(IFormFile Image);
    }
}
