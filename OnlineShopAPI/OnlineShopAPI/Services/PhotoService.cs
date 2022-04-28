using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using OnlineShopAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineShopAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _uow;

        public PhotoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<bool> AddPhotoToProduct(int productId, IFormFile photo)
        {
            return Task.FromResult(true);
        }

        public Task<bool> AddPhotoToUser(int userId, IFormFile photo)
        {
            return Task.FromResult(true);
        }
    }
}
