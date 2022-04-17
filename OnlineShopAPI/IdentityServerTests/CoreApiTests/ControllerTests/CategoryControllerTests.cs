using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineShopAPI.Controllers;
using OnlineShopAPI.DTO.Category;
using OnlineShopAPI.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShopAPI.Tests
{
    public class CategoryControllerTests
    {
        private readonly CategoryController _categoryController;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<ICategoryRepository> _categoryRepository;

        public CategoryControllerTests()
        {
            var mockLogger = new Mock<ILogger>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper() as IMapper;
            _mockUow = new Mock<IUnitOfWork>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _mockUow.SetupGet(uow => uow.CategoryRepository).Returns(_categoryRepository.Object);

            _categoryController = new CategoryController(mockLogger.Object, mapper, _mockUow.Object);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnBadRequest_WhenCategoriesNotFound()
        {
            _categoryRepository
                .Setup(cr => cr.GetAllAsync())
                .ReturnsAsync(new List<Category>());

            var response = await _categoryController.GetCategories() as ActionResult<IEnumerable<CategoryDto>>;

            var result = response.Result as ObjectResult;
            var value = result?.Value as string;

            result?.Value.Should().Be("There are not any categories");

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void DeleteCategory_ShouldReturnBadRequest_WhenCategoryNotFound()
        {
            var id = 1;

            _categoryRepository
                .Setup(cr => cr.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Category);

            var response = _categoryController.DeleteCategory(id);

            var result = response.Result.Result as ObjectResult;
            var value = result?.Value as string;

            result?.Value.Should().Be(string.Format("Category with id {0} not found", id));

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
