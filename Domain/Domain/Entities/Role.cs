﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}