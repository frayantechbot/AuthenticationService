﻿namespace Authentication.Domain.Entities;

public class PasswordReset : BaseEntity
{
    public string? Token { get; set; }
    public bool IsUsed { get; set; }
    
    public virtual UserChannel? UserChannel { get; set; }
}