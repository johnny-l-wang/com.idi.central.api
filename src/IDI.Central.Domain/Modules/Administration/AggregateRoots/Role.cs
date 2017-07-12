﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    public class Role : AggregateRoot
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Descrition { get; set; }

        public bool IsActive { get; set; } = true;

        public List<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}