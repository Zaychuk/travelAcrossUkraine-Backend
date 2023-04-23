using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface IUserRepository
{
    Task<UserEntity> GetAsync(string username, string passwordHash);
    Task CreateAsync(UserEntity user);
}

public class UserRepository : IUserRepository
{
    private readonly TravelAcrossUkraineContext _context;

    public UserRepository(TravelAcrossUkraineContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(UserEntity user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserEntity> GetAsync(string username, string passwordHash)
    {
        return await _context.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower() && user.PasswordHash == passwordHash);
    }
}
