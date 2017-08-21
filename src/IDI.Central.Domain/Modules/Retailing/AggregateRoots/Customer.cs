﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class Customer : AggregateRoot
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(200)]
        public string DeliveryAddress { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public Guid UserId { get; set; }
    }
}