using AI.Core.Interfaces.Service;
using AI.Repository.Utility;
using Microsoft.AspNetCore.Http;

namespace AI.Services
{
    public class ClassifierService : IClassifierService
    {
        public async Task<string> Classifier(IFormFile Image)
        {
            string answer = "null";
            string url = "https://8000-01j05y64r4v7f8xp0y3d9kaz0v.cloudspaces.litng.ai/predict/";
            string filePath = DocumentSettings.UploadFile(Image, "Classifier");

            using (var httpClient = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        form.Add(new StreamContent(fileStream), "file", Path.GetFileName(filePath));

                        using (var response = await httpClient.PostAsync(url, form))
                        {
                            answer = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            return answer;
        }
    }
}
