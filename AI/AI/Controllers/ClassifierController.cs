using AI.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace AI.Controllers
{
    public class ClassifierController : APIBaseController
    {
        private readonly IClassifierService _ClassifierService;
        public ClassifierController(IClassifierService ClassifierService)
        {
            _ClassifierService = ClassifierService;
        }
        [HttpPost("Classifier")]
        public async Task<string> Classifier(IFormFile input)
        {
            string x = await _ClassifierService.Classifier(input);
            return x;
        }
    }
}
