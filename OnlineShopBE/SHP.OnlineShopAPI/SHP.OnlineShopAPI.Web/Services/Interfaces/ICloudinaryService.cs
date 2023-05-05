using CloudinaryDotNet.Actions;
using System.Threading.Tasks;

namespace SHP.OnlineShopAPI.Web.Services.Interfaces
{
    public interface ICloudinaryService
    {
        bool Setup();

        Task<ImageUploadResult> UploadAsync(ImageUploadParams parameters);
    }
}
