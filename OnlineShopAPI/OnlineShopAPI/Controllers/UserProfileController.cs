using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.Constants;
using OnlineShopAPI.Services.Interfaces;
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
            return await _photoService.AddPhotoToUser(this.GetUserId(), photo) 
                ? Ok((await _uow?.UserRepository.FindAsync(this.GetUserId())).PhotoUrl) 
                : BadRequest("Unable to upload the photo");
        }

        [Authorize(Roles = Roles.AdminOrModerOrSeller)]
        [HttpPost("photo-to-product")]
        public async Task<ActionResult> AddPhotoToProduct(int productId, IFormFile photo)
        {
            var user = await _uow.UserRepository.GetUserWithProductsWithPhotosAsync(this.GetUserId());
            var result = await _photoService.AddPhotoToProduct(user, productId, photo);

            return result 
                ? Ok() 
                : BadRequest("Unable to upload the photo");
        }
    }
}
