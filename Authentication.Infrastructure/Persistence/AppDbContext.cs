﻿using Microsoft.EntityFrameworkCore;
using Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Authentication.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseNpgsql(configuration.GetSection("ConnectionStrings")["Default"]);
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<UserChannel>? UserChannels { get; set; }
    public DbSet<Verification>? Verifications { get; set; }
    public DbSet<Login>? Logins { get; set; }
    public DbSet<PasswordReset>? PasswordResets { get; set; }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entity)
            {
                switch (entry)
                {
                    case { State: EntityState.Added }:
                        entity.Id = Guid.NewGuid();
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case { State: EntityState.Modified }:
                        entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
        }

        return base.SaveChanges();
    }
}