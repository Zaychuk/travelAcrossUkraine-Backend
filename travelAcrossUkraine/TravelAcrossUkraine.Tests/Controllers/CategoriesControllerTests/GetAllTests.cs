using TravelAcrossUkraine.Tests.Mocks;
using TravelAcrossUkraine.WebApi.Controllers;

namespace TravelAcrossUkraine.Tests.Controllers.CategoriesControllerTests;

public class GetAllTests
{
    private readonly TravelAcrossUkraineContextMock _context;
    private readonly CategoriesController _categoriesController;

    public GetAllTests()
    {
        _context = DependencyFactory.GetContext<GetAllTests>();
        DatabasePopulator.EnsureFreshDatabase(_context);
        _categoriesController = DependencyFactory.GetCategoriesController(_context);
    }

    [Fact]
    public async Task GetAllCategories_Success_WhenEmpty()
    {
        // Act
        var result = await _categoriesController.GetAllAsync();

        // Assert
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task GetAllCategories_Success()
    {
        // Arrange
        var categoryName = "Category";
        var typeName = "Type";
        var typeId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();
        await DatabasePopulator.CreateTypeAsync(_context, typeId, typeName);
        await DatabasePopulator.CreateCategotyAsync(_context, categoryId, typeId, categoryName);

        // Act
        var result = await _categoriesController.GetAllAsync();

        // Assert
        Assert.Single(result.Value);
        Assert.Contains(result.Value, category => category.Name == categoryName && category.Type.Name == typeName);
    }
}
