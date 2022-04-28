using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OnlineShopAPI.Options;
using OnlineShopAPI.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnlineShopAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private Cloudinary _cloudinary;

        public PhotoService(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;

            SetupCloudinaryService();
        }

        public Task<bool> AddPhotoToProduct(int productId, IFormFile photo)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(photo.FileName)
            };
            var uploadResult = _cloudinary.UploadAsync(uploadParams);

            return Task.FromResult(true);
        }

        public async Task<bool> AddPhotoToUser(int userId, IFormFile photo)
        {
            if (photo == null)
            {
                return false;
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), photo.OpenReadStream()),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            var linkToPhoto = uploadResult.Url;

            var user = await _uow.UserRepository.FindAsync(userId);

            if (user == null)
            {
                return false;
            }

            user.PhotoUrl = linkToPhoto.AbsoluteUri;

            await _uow.UserRepository.UpdateAsync(user);

            return await Task.FromResult(true);
        }

        private void SetupCloudinaryService()
        {
            var account = new Account
            {
                Cloud = _configuration.GetSection(ConfigurationOptions.CloudName).Value,
                ApiKey = _configuration.GetSection(ConfigurationOptions.ApiKey).Value,
                ApiSecret = _configuration.GetSection(ConfigurationOptions.ApiSecret).Value
            };

            _cloudinary = new Cloudinary(account);
        }
    }
}
