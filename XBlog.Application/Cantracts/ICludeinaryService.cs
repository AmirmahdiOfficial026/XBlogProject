using Microsoft.AspNetCore.Http;

namespace XBlog.Application.Cantracts;

public interface ICludeinaryService
{
    Task<string> UploadImageAsync(IFormFile file, string folder = "general");
    Task<string> UploadImageFromUrlAsync(string imageUrl, string folder = "general");
    Task<bool> DeleteImageAsync(string imageUrl);
    string GetOptimizedUrl(string publicId, int width = 1200, int height = 900);
    string GetPublicIdFromUrl(string imageUrl);
}
