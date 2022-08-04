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
using System.Linq;
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
        public async Task GetCategories_ShouldReturnNoContent_WhenCategoriesNotFound()
        {
            _categoryRepository
                .Setup(cr => cr.GetAllAsync())
                .ReturnsAsync(new List<Category>());

            var response = await _categoryController.GetCategories();

            var result = response.Result as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnOkWithCategories_WhenCatigoriesArePresent()
        {
            _categoryRepository
                .Setup(cr => cr.GetAllAsync())
                .ReturnsAsync(new List<Category> { new Category() });

            var response = await _categoryController.GetCategories();

            var result = response.Result as ObjectResult;

            result?.Value.Should().NotBeNull().And.Match<IEnumerable<CategoryDto>>(categories => categories.Any());

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetCategoryByName_ShouldReturnNoContent_WhenCategoryNotFound()
        {
            _categoryRepository
                .Setup(cr => cr.GetCategoryByName(It.IsAny<string>()))
                .ReturnsAsync(null as Category);

            var response = await _categoryController.GetCategoryByName("name");

            var result = response.Result as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task GetCategoryByName_ShouldReturnOkWithCategory_WhenCategoryFound()
        {
            var categoryName = "Some name";

            _categoryRepository
                .Setup(cr => cr.GetCategoryByName(It.Is<string>(name => name == categoryName)))
                .ReturnsAsync(new Category { Name = categoryName });

            var response = await _categoryController.GetCategoryByName(categoryName) as ActionResult<CategoryDto>;

            var result = response.Result as ObjectResult;
            var value = result.Value as CategoryDto;
            value.Should().Match<CategoryDto>(category => category.Name == categoryName);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task CreateCategory_ShouldReturnOkWithCategoryDto_WhenCategoryCreated()
        {
            var categoryName = "name";
            var categoryDto = new CreateCategoryDto { Name = categoryName };

            _categoryRepository
                .Setup(cr => cr.GetCategoryByName(It.Is<string>(s => s == categoryName)))
                .ReturnsAsync(new Category { Name = categoryName });

            var response = await _categoryController.CreateCategory(categoryDto) as ActionResult<CategoryDto>;

            var result = response.Result as ObjectResult;
            var value = result.Value as CategoryDto;
            value.Should().Match<CategoryDto>(category => category.Name == categoryName);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task CreateCategory_ShouldReturnBadRequest_WhenCategoryNotCreated()
        {
            var categoryName = "name";
            var categoryDto = new CreateCategoryDto { Name = categoryName };

            _categoryRepository
                .Setup(cr => cr.GetCategoryByName(It.Is<string>(s => s == categoryName)))
                .ReturnsAsync(null as Category);

            var response = await _categoryController.CreateCategory(categoryDto) as ActionResult<CategoryDto>;

            var result = response.Result as ObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task ChangeCategory_ShouldReturnBadRequest_WhenCategoryNotFound()
        {
            var categoryName = "name";
            var categoryDto = new ChangeCategoryDto { Name = "new Name" };

            _categoryRepository
                .Setup(cr => cr.GetCategoryByName(It.Is<string>(s => s == categoryName)))
                .ReturnsAsync(null as Category);

            var response = await _categoryController.ChangeCategory(categoryName, categoryDto);

            var result = response as ObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task ChangeCategory_ShouldReturnNoContent_WhenCategoryFound()
        {
            var categoryName = "name";
            var categoryDto = new ChangeCategoryDto { Name = "new Name" };

            _categoryRepository
                .Setup(cr => cr.GetCategoryByName(It.Is<string>(s => s == categoryName)))
                .ReturnsAsync(new Category { Name = categoryDto.Name });

            var response = await _categoryController.ChangeCategory(categoryName, categoryDto);

            var result = response as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async void DeleteCategory_ShouldReturnBadRequest_WhenCategoryNotFound()
        {
            var id = 1;

            _categoryRepository
                .Setup(cr => cr.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Category);

            var response = await _categoryController.DeleteCategory(id);

            var result = response.Result as ObjectResult;
            var value = result?.Value as string;

            result?.Value.Should().Be(string.Format("Category with id {0} not found", id));

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnNoContent_WhenDeleted()
        {
            var id = 1;

            _categoryRepository
                .Setup(cr => cr.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(new Category { Id = id });

            var response = await _categoryController.DeleteCategory(id);

            var result = response.Result as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
