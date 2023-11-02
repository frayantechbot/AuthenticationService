﻿using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;
using Authentication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IRepository<User>, EntityRepository<User>>();
builder.Services.AddScoped<ICrudService<User, UserCreateDto, UserUpdateDto>, UserService>();

builder.Services.AddScoped<IRepository<UserChannel>, EntityRepository<UserChannel>>();
builder.Services.AddScoped<ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto>, UserChannelService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();