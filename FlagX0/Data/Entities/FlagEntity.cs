﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlagX0.Data.Entities
{
    public class FlagEntity
    {
        [Key] public int Id { get; set; }
        public required string Name { get; set; }
        public IdentityUser User { get; set; }
        public required virtual string UserId { get; set; }
        public required bool Value { get; set; }
    }
}
