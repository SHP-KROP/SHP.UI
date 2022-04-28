using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OnlineShopAPI.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<bool> AddPhotoToUser(int userId, IFormFile photo);

        Task<bool> AddPhotoToProduct(int productId, IFormFile photo);
    }
}
