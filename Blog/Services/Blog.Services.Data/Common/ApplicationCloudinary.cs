namespace Blog.Services.Data.Common
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public static class ApplicationCloudinary
    {
        public static async Task<string> UploadViaFromFile(Cloudinary cloudinary, IFormFile image, string name)
        {
            byte[] destinationImage;

            await using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);

                destinationImage = memoryStream.ToArray();
            }

            var result = await UploadAsync(cloudinary, destinationImage, name);

            return result;
        }

        public static async Task<string> UploadImageViaLink(Cloudinary cloudinary, string link, string name)
        {
            var webClient = new System.Net.WebClient();
            var bytes = await webClient.DownloadDataTaskAsync(link);

            var result = await UploadAsync(cloudinary, bytes, name);

            return result;
        }

        public static async Task DeleteImage(Cloudinary cloudinary, string name)
        {
            var delParams = new DelDerivedResParams
            {
                DerivedResources = new List<string>
                {
                    name
                },
            };

            await cloudinary.DeleteDerivedResourcesAsync(delParams);
        }

        private static async Task<string> UploadAsync(Cloudinary cloudinary, byte[] bytes, string name)
        {
            await using var ms = new MemoryStream(bytes);

            // Cloudinary doesn't work with &
            name = name.Replace("&", "And");

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(name, ms),
                PublicId = name,
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUri.AbsoluteUri;
        }
    }
}
