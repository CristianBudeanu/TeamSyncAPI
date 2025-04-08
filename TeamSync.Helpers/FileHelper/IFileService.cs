using Microsoft.AspNetCore.Http;

namespace TeamSync.Helpers.FileHelper
{
    public interface IFileService
    {
        Task<string> SaveImage(IFormFile file, Guid proiectId);
    }
}
