namespace PropertyManagement.API.Services
{
    public interface ICloudStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
        Task<(string PublicId, string Url)> UploadImageAsync(IFormFile file, string folder);
        Task<Stream> DownloadFileAsync(string publicId);
        Task<bool> DeleteFileAsync(string publicId);
    }
}