using Microsoft.AspNetCore.Http;

namespace TeamSync.Helpers.ImageHelper
{
    public class ImageService : IImageService
    {
        public async Task<string> SaveImage(IFormFile image)
        {
            string projectImages = @"D:\\Projects\\TeamSync\\Resources\\ProjectImages";
            string imagePath = Path.Combine(projectImages, image.FileName);

            using (Stream fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
                return imagePath;
            }
        }
    }
}
