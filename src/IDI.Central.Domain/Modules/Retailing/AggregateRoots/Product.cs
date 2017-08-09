﻿using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class Product : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public string Tags { get; set; }

        public string Price { get; set; }

        public string Picture { get; set; }

        public bool Enabled { get; set; } = true;

    }
}