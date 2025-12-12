using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PropertyManagement.API.Models.Cloudinary;
using PropertyManagement.API.Services;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudStorageController : ControllerBase
    {
        private readonly ICloudStorageService cloudStorageService;
        private readonly IOptions<CloudinarySettings> cloudinaryOptions;

        public CloudStorageController(ICloudStorageService cloudStorageService, IOptions<CloudinarySettings> cloudinaryOptions)
        {
            this.cloudStorageService = cloudStorageService;
            this.cloudinaryOptions = cloudinaryOptions;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadToCloudinary([FromForm] FileUploadRequest request)
        {
            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("Please upload a valid file");
            }

            var publicId = await cloudStorageService.UploadFileAsync(request.File, folder: null);

            string url = $"https://res.cloudinary.com/{cloudinaryOptions.Value.CloudName}/raw/upload/{publicId}";

            return Ok(new
            {
                message = "File uploaded successfully",
                publicId,
                url
            });
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImageToCloudinary([FromForm] FileUploadRequest request)
        {
            if (request?.File == null || request.File.Length == 0)
            {
                return BadRequest("Please upload a valid image file.");
            }

            var (publicId, url) = await cloudStorageService.UploadImageAsync(request.File, folder: "property-images");

            return Ok(new
            {
                message = "Image uploaded successfully",
                publicId,
                url
            });
        }

        [HttpGet("download/{publicId}")]
        public async Task<IActionResult> DownloadFromCloudinary(string publicId)
        {
            publicId = Uri.UnescapeDataString(publicId);

            string fileName = publicId.Contains('/') ? publicId.Split('/').Last() : publicId;

            var stream = await cloudStorageService.DownloadFileAsync(publicId);

            return File(((MemoryStream)stream).ToArray(), "application/octet-stream", fileName);
        }

        [HttpDelete("delete/{publicId}")]
        public async Task<IActionResult> DeleteFromCloudinary(string publicId)
        {
            var decodedPublicId = Uri.UnescapeDataString(publicId);

            var deleted = await cloudStorageService.DeleteFileAsync(decodedPublicId);

            if (!deleted)
            {
                return BadRequest("Failed to delete file from Cloudinary");
            }

            return Ok(new 
            { 
                message = "File deleted successfully", 
                publicId 
            });
        }
    }
}