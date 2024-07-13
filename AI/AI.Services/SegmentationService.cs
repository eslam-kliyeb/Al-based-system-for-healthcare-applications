using AI.Core.Interfaces.Service;
using AI.Repository.Utility;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace AI.Services
{
    public class SegmentationService : ISegmentationService
    {
        public async Task<string> Segmentation(IFormFile InputImage)
        {
            string formFile = "5555";
            string url = "https://8001-01j15p7wbate9y1mbsexv5bvr4.cloudspaces.litng.ai/segmentation/";
            string imagePath = DocumentSettings.UploadFile(InputImage, "Mri");

            using (var client = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        var streamContent = new StreamContent(fileStream);
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                        form.Add(streamContent, "file", "image.jpg");

                        try
                        {
                            HttpResponseMessage response = await client.PostAsync(url, form);

                            if (response.IsSuccessStatusCode)
                            {
                                // Define the filename where you want to save the segmented image
                                string segmentedImageName = $"{Guid.NewGuid()}-segmented_image.jpg";
                                string segmentedImagePath = $"wwwroot/Images/Segmentation/{segmentedImageName}";

                                using (var responseStream = await response.Content.ReadAsStreamAsync())
                                {
                                    using (var segmentedImage = Image.FromStream(responseStream))
                                    {
                                        // Save the segmented image with the specified filename
                                        segmentedImage.Save(segmentedImagePath);
                                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", "Segmentation");
                                        var fileName = segmentedImageName;
                                        var filePath = Path.Combine(folderPath, fileName);
                                        return segmentedImageName;
                                    }
                                }
                            }
                            else
                            {
                                string error = await response.Content.ReadAsStringAsync();
                                return ($"Error: {response.StatusCode} - {error}");
                            }
                        }
                        catch (Exception ex)
                        {
                            return ($"Exception occurred: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
