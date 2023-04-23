using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface IRoleRepository
{
    Task<RoleEntity> GetAsync(string name);
}

public class RoleRepository : IRoleRepository
{
    private readonly TravelAcrossUkraineContext _context;

    public RoleRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }

    public async Task<RoleEntity> GetAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == name);
    }
}
