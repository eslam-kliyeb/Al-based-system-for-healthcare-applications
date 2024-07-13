using AI.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace AI.Controllers
{
    public class SegmentationController : APIBaseController
    {
        private readonly ISegmentationService _SegmentationService;
        public SegmentationController(ISegmentationService SegmentationService)
        {
            _SegmentationService = SegmentationService;
        }
        [HttpPost("Segmentation")]
        public async Task<string> Segmentation(IFormFile input)
        {
            string x = await _SegmentationService.Segmentation(input);
            return x;
        }
    }
}
