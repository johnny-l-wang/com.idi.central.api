﻿using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class OrderItem : AggregateRoot
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? ReadjustUnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}