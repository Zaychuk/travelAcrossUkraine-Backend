﻿namespace TravelAcrossUkraine.WebApi.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    public string Role { get; set; }
    public string Surname { get; set; }
    public string GivenName { get; set; }
}
