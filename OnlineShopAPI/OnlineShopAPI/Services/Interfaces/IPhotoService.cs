using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OnlineShopAPI.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<bool> AddPhotoToUser(int userId, IFormFile photo);

        Task<bool> AddPhotoToProduct(AppUser user, int productId, IFormFile photo);
    }
}
