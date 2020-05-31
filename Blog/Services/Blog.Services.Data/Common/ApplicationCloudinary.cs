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
        public static async Task<string> UploadImage(Cloudinary cloudinary, IFormFile image, string name)
        {
            byte[] destinationImage;

            await using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);

                destinationImage = memoryStream.ToArray();
            }

            await using var ms = new MemoryStream(destinationImage);

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
    }
}
