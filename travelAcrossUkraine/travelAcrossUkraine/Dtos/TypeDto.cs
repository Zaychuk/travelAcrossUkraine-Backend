﻿namespace TravelAcrossUkraine.WebApi.Dtos;

public class TypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CategoryWithoutTypeDto> Categories { get; set; }
}
