using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface IImageRepository
{
    Task CreateRangeAsync(List<ImageEntity> images);
}

public class ImageRepository : IImageRepository
{
    private readonly TravelAcrossUkraineContext _context;

    public ImageRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }

    public async Task CreateRangeAsync(List<ImageEntity> images)
    {
        await _context.Images.AddRangeAsync(images);
    }
}
