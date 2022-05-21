using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using OnlineShopAPI.Options;
using OnlineShopAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineShopAPI.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration _configuration;
        private Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Setup()
        {
            try
            {
                var account = new Account
                {
                    Cloud = _configuration.GetSection(ConfigurationOptions.Cloudinary.CloudName).Value,
                    ApiKey = _configuration.GetSection(ConfigurationOptions.Cloudinary.ApiKey).Value,
                    ApiSecret = _configuration.GetSection(ConfigurationOptions.Cloudinary.ApiSecret).Value
                };

                _cloudinary = new Cloudinary(account);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<ImageUploadResult> UploadAsync(ImageUploadParams parameters)
        {
            return _cloudinary.UploadAsync(parameters);
        }
    }
}
