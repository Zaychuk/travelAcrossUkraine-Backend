using TravelAcrossUkraine.Tests.Mocks;
using TravelAcrossUkraine.WebApi.Controllers;

namespace TravelAcrossUkraine.Tests.Controllers.CollectionsControllerTests;

public class GetListTests
{
    private readonly TravelAcrossUkraineContextMock _context;
    private readonly TypesController _collectionsController;

    public GetListTests()
    {
        _context = DependencyFactory.GetContext<GetListTests>();
        DatabasePopulator.EnsureFreshDatabase(_context);
        _collectionsController = DependencyFactory.GetTypesController(_context);
    }

    [Fact]
    public async Task GetAllCollections_Success_WhenEmpty()
    {
        // Act
        var result = await _collectionsController.GetAllAsync();

        // Assert
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task GetAllCategories_Success()
    {
        // Arrange
        var name = "Type";
        var typeId = Guid.NewGuid();
        await DatabasePopulator.CreateTypeAsync(_context, typeId, name);

        // Act
        var result = await _collectionsController.GetAllAsync();

        // Assert
        Assert.Single(result.Value);
        Assert.Contains(result.Value, type => type.Name == name);
    }
}
