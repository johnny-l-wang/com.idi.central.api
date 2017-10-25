﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Authentication;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("Roles")]
    public class Role : AggregateRoot, IRole
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Descrition { get; set; }

        public bool IsActive { get; set; } = true;

        public string Permissions { get; set; }

        public string Menus { get; set; }
    }
}
