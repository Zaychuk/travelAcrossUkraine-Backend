﻿namespace TravelAcrossUkraine.WebApi.Dtos;

public class CreateUserDto
{
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    public string Surname { get; set; }
    public string GivenName { get; set; }
    public string Password { get; set; }
}
