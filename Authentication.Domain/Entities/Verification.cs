﻿namespace Authentication.Domain.Entities;

public class Verification : BaseEntity
{
    public required string Code { get; set; }

    public bool IsVerified { get; set; }

    public virtual User? User { get; set; }
}