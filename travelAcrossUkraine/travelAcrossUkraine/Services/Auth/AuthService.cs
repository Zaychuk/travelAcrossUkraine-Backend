using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelAcrossUkraine.WebApi.Dtos;
using TravelAcrossUkraine.WebApi.Dtos.Auth;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Exceptions;
using TravelAcrossUkraine.WebApi.Helpers;
using TravelAcrossUkraine.WebApi.Repositories;
using TravelAcrossUkraine.WebApi.Utility;

namespace TravelAcrossUkraine.WebApi.Services.Auth;

public interface IAuthService
{
    Task<string> AuthenticateAsync(UserLoginDto userLogin);
    Task SignUpAsync(CreateUserDto userDto);
}

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public AuthService(IConfiguration configuration, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task SignUpAsync(CreateUserDto userDto)
    {
        var userEntity = _mapper.Map<UserEntity>(userDto);
        userEntity.PasswordHash = HashHelper.HashString(userDto.Password);
        userEntity.RoleId = (await _roleRepository.GetAsync(RoleNames.User)).Id;
        BaseEntityHelper.SetBaseProperties(userEntity);

        await _userRepository.CreateAsync(userEntity);
    }

    public async Task<string> AuthenticateAsync(UserLoginDto userLogin)
    {
        var passwordHash = HashHelper.HashString(userLogin.Password);
        var user = await _userRepository.GetAsync(userLogin.Username, passwordHash) ?? throw new ForbiddenException();

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.GivenName, user.GivenName),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Role, user.Role.Name),
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
