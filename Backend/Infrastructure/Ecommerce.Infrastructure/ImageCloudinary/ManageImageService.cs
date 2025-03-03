using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.ImageManagement;
using Microsoft.Extensions.Options;

namespace Ecommerce.Infrastructure.ImageCloudinary;

public class ManageImageService(IOptions<CloudinarySettings> cloudinarySettings) : IManageImageService
{
    private CloudinarySettings CloudinarySettings { get; } = cloudinarySettings.Value;

    public async Task<ImageResponse> UploadImage(ImageData imageStream)
    {
        var account = new Account(
            CloudinarySettings.CloudName,
            CloudinarySettings.ApiKey,
            CloudinarySettings.ApiSecret
        );
        
        var cloudinary = new Cloudinary(account);
        var uploadImage = new ImageUploadParams()
        {
            File = new FileDescription(imageStream.Name, imageStream.ImageStream)
        };
        
        var uploadResult = await cloudinary.UploadAsync(uploadImage);
        if (uploadResult.StatusCode == HttpStatusCode.OK)
        {
            return new ImageResponse
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString()
            };
        }
        
        throw new Exception("Upload Failed");
    }
}