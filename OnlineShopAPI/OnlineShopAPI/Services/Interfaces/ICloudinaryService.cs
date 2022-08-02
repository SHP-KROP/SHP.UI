using CloudinaryDotNet.Actions;
using System.Threading.Tasks;

namespace OnlineShopAPI.Services.Interfaces
{
    public interface ICloudinaryService
    {
        bool Setup();

        Task<ImageUploadResult> UploadAsync(ImageUploadParams parameters);
    }
}
