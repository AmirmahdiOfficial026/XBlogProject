using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using XBlog.Application.Cantracts;

namespace XBlog.Application.Serivces;

public class CludeInaryService : ICludeinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly string _cloudName;
    public CludeInaryService(IConfiguration configurarion)
    {
        var CloudName = configurarion["Cloudinary:CloudName"];
        var apiKey = configurarion["Cloudinary:ApiKey"];
        var apiSecret = configurarion["Cloudinary:ApiSecret"];
        _cloudName = CloudName;
        var account = new Account(CloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account);
    }
    public async Task<bool> DeleteImageAsync(string imageUrl)
    {
        try
        {
            var publicId = GetPublicIdFromUrl(imageUrl);
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok";
        }
        catch
        {
            return false;
        }
    }

    public string GetOptimizedUrl(string publicId, int width = 1200, int height = 900)
    {
        try
        {
            return _cloudinary.Api.ApiUrlImgUp.Transform(new Transformation()
                .Width(width).Height(height).Crop("fill").Quality("auto")
                .FetchFormat("auto")).BuildUrl(publicId);
        }
        catch
        {
            return "مشکلی در فرایند آپلود عکس وجود دارد";
        }
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folder = "general")
    {
        try
        {
            if (file == null || file.Length == 0)
                throw new Exception("فایل انتخاب نشده");
            var allowExtentions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };
            var extentions = System.IO.Path.GetExtension(file.FileName).ToLower();
            if (!allowExtentions.Contains(extentions))
                throw new ArgumentException("فرمت فایل مجاز نیست");
            if (file.Length > 10 * 1024 * 1024)
                throw new ArgumentException("حجم فایل بیشتر از 10 مگابایت است");
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
                Transformation = new Transformation()
                .Crop("fill").Quality("auto").FetchFormat("auto")
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.ToString());
            return uploadResult.SecureUri.ToString();
        }
        catch
        {
            return "مشکلی در فرایند آپلود عکس وجود دارد";
        }
    }

    public async Task<string> UploadImageFromUrlAsync(string imageUrl, string folder = "general")
    {
        try
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(imageUrl),
                Folder = folder,
                Transformation = new Transformation()
                    .Crop("fill").Quality("auto").FetchFormat("auto")
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.ToString());
            return uploadResult.SecureUri.ToString();
        }
        catch
        {
            return "مشکلی در فرایند آپلود عکس وجود دارد";
        }
    }
    public string GetPublicIdFromUrl(string imageUrl)
    {
        try
        {
            var uri = new Uri(imageUrl);
            var segments = uri.Segments;
            var fileName = segments[^1];
            var publicId = Path.GetFileNameWithoutExtension(fileName);
            if (segments.Length > 3)
            {
                var folder = segments[^2].TrimEnd('/');
                publicId = $"{folder}/{publicId}";
            }

            Console.WriteLine($"✅ Extracted Public ID: {publicId}");
            return publicId;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
            return null;
        }
    }
}
