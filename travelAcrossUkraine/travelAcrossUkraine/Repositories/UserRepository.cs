using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Entities;

namespace TravelAcrossUkraine.WebApi.Repositories;

public interface IUserRepository
{
    Task<UserEntity> GetAsync(string username, string passwordHash);
    Task CreateAsync(UserEntity user);
    Task<UserEntity> GetAsync(string username);
    Task<List<UserEntity>> GetAllAsync();
    Task<UserEntity> GetByIdAsync(Guid id);
    Task DeleteAsync(UserEntity user);
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

    public async Task DeleteAsync(UserEntity user)
    {
        user.IsDeleted = true;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<UserEntity>> GetAllAsync()
    {
        return await _context.Users
            .Include(user => user.Role)
            .ToListAsync();
    }

    public async Task<UserEntity> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<UserEntity> GetAsync(string username, string passwordHash)
    {
        return await _context.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower() && user.PasswordHash == passwordHash);
    }

    public async Task<UserEntity> GetAsync(string username)
    {
        return await _context.Users
            .Include(user => user.Role)
            .Include(user => user.Collections)
            .FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
    }
}
