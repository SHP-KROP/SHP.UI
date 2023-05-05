using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SHP.OnlineShopAPI.Web.Constants;
using SHP.OnlineShopAPI.Web.DTO.Category;
using SHP.OnlineShopAPI.Web.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHP.OnlineShopAPI.Web.Controllers
{
    [Authorize(Roles = Roles.AdminOrModer)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public CategoryController(ILogger logger, IMapper mapper, IUnitOfWork uow)
        {
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _uow?.CategoryRepository.GetAllAsync();

            if (categories is null || categories.Count() == 0)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{categoryName}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByName(string categoryName)
        {
            var category = await _uow?.CategoryRepository.GetCategoryByName(categoryName);

            if (category is null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);

            await _uow?.CategoryRepository.AddAsync(category);

            await _uow?.ConfirmAsync();

            var createdCategory = await _uow?.CategoryRepository.GetCategoryByName(createCategoryDto.Name);

            if (createdCategory is null)
            {
                return BadRequest("Unable to create category");
            }

            return Ok(_mapper.Map<CategoryDto>(createdCategory));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{categoryName}")]
        public async Task<ActionResult> ChangeCategory(string categoryName, [FromBody] ChangeCategoryDto changeCategoryDto)
        {
            var category = await _uow?.CategoryRepository.GetCategoryByName(categoryName);

            if (category is null)
            {
                return BadRequest(string.Format("Category with name {0} not found", categoryName));
            }

            category.ProjectFrom(changeCategoryDto);

            _uow?.CategoryRepository.Update(category);

            await _uow?.ConfirmAsync();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(int id)
        {
            var category = await _uow?.CategoryRepository.GetAsync(id);

            if (category is null)
            {
                return BadRequest(string.Format("Category with id {0} not found", id));
            }

            _uow?.CategoryRepository.Remove(category);

            await _uow.ConfirmAsync();

            return NoContent();
        }
    }
}
