using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using System.Drawing.Imaging;
using System.Drawing;

namespace AI.Repository.ApiHelper
{
    public static class Convert
    {
        public static IFormFile ConvertBitmapToIFormFile(Bitmap bitmap, string fileName)
        {
            IFormFile ans = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the bitmap to the memory stream
                bitmap.Save(memoryStream, ImageFormat.Jpeg);

                // Set the position to the beginning of the stream
                memoryStream.Position = 0;

                // Create an IFormFile instance
                var formFile = new FormFile(memoryStream, 0, memoryStream.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg" // Set the content type according to your image format
                };
                return formFile;
            }
        }
    }
}
