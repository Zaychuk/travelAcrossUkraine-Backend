using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.WebApi.Entities;

public class UserEntity :BaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string EmailAdress { get; set; }
    public string Surname { get; set; }
    public string GivenName { get; set; }
    public RoleEntity Role { get; set; }
    public Guid RoleId { get; set; }
}
