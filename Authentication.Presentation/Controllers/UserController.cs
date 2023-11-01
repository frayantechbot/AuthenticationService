﻿using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;

namespace Authentication.Presentation.Controllers;

public class UserController : SafeDeleteCrudController<User, UserCreateDto, UserUpdateDto>
{
    public UserController(ICrudService<User, UserCreateDto, UserUpdateDto> service) : base(service)
    {
    }
}