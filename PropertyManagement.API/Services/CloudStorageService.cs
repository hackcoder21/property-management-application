using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace PropertyManagement.API.Services
{
    public class CloudStorageService : ICloudStorageService
    {
        private readonly Cloudinary cloudinary;

        public CloudStorageService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("File is empty");
            }

            using var stream = file.OpenReadStream();

            var extension = Path.GetExtension(file.FileName).ToLower();

            // Determine if file is an image
            bool isImage = extension == ".jpg" || extension == ".jpeg" ||
                           extension == ".png" || extension == ".gif" ||
                           extension == ".webp";

            folder ??= isImage ? "images" : "templates";

            if (isImage)
            {
                // Upload images
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = folder,
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true,
                    Invalidate = true
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Failed to upload image");
                }

                return uploadResult.PublicId;
            }
            else
            {
                // Upload raw files
                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = folder,
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true,
                    Invalidate = true
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Failed to upload file");
                }

                return uploadResult.PublicId;
            }
        }

        public async Task<(string PublicId, string Url)> UploadImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty", nameof(file));
            }

            using var stream = file.OpenReadStream();

            folder ??= "images";

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                Invalidate = true
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult == null || uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Failed to upload image to Cloudinary");
            }

            var publicId = uploadResult.PublicId;
            var url = uploadResult.SecureUrl?.ToString() ?? uploadResult.Url?.ToString();

            return (publicId, url);
        }

        public async Task<Stream> DownloadFileAsync(string publicId)
        {
            var http = new HttpClient();

            var resource = await cloudinary.GetResourceAsync(new GetResourceParams(publicId)
            {
                ResourceType = ResourceType.Raw
            });

            string downloadUrl = resource.SecureUrl;

            var bytes = await http.GetByteArrayAsync(downloadUrl);

            return new MemoryStream(bytes);
        }

        public async Task<bool> DeleteFileAsync(string publicId)
        {
            var rawResult = await cloudinary.DestroyAsync(new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Raw
            });

            if (rawResult.Result == "ok")
            {
                return true;
            }

            var imageResult = await cloudinary.DestroyAsync(new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image
            });

            return imageResult.Result == "ok";
        }
    }
}