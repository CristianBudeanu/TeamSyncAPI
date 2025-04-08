using Microsoft.AspNetCore.Http;

namespace TeamSync.Helpers.FileHelper
{
    public class FileService : IFileService
    {
        public async Task<string> SaveImage(IFormFile file, Guid proiectId)
        {
            string projectFilesPath;

#if DEBUG
            projectFilesPath = $"D:\\Projects\\TeamSync\\Resources\\Projects\\{proiectId}";
#elif RELEASE
    projectFilesPath = Path.Combine("D:\\Projects\\TeamSync\\Resources\\ProjectImages", proiectId.ToString());
#else
    throw new InvalidOperationException("Unknown build configuration");
#endif

            // Ensure directory exists
            if (!Directory.Exists(projectFilesPath))
            {
                Directory.CreateDirectory(projectFilesPath);
            }

            // Get file name and extension
            string fileName;
            string extension = Path.GetExtension(file.FileName);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);

            string filePath;
            int counter = 1;

            do
            {
                fileName = $"{fileNameWithoutExt}_{counter}{extension}";
                filePath = Path.Combine(projectFilesPath, fileName);
                counter++;
            } while (File.Exists(filePath));

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return proiectId + "/" + fileName;
        }
    }
}
