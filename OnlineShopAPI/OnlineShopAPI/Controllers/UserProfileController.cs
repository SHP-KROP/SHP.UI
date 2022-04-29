using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.Options;
using OnlineShopAPI.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IPhotoService _photoService;

        public UserProfileController(IUnitOfWork uow, IPhotoService photoService)
        {
            _uow = uow;
            _photoService = photoService;
        }

        [Authorize]
        [HttpPost("photo-to-user")]
        public async Task<ActionResult> AddPhotoToUser(IFormFile photo)
        {
            return await _photoService.AddPhotoToUser(GetUserId(), photo) 
                ? Ok(new AppUser()) 
                : BadRequest("Unable to upload the photo");
        }

        [Authorize]
        [HttpPost("photo-to-product")]
        public async Task<ActionResult> AddPhotoToProduct(int productId, IFormFile photo)
        {
            var user = await _uow.UserRepository.GetUserWithProductsWithPhotosAsync(GetUserId());
            var result = await _photoService.AddPhotoToProduct(user, productId, photo);
            await _uow?.ConfirmAsync();

            return result ? Ok() : BadRequest("Unable to upload the photo");
        }

        private int GetUserId()
        {
            int.TryParse(User.Claims.First(x => x.Type == JwtClaimOptions.AuthorizationNameId).Value, out var id);

            return id;
        }
    }
}
