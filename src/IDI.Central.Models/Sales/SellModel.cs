﻿using System;
using System.Collections.Generic;
using IDI.Central.Common.JsonTypes;
using IDI.Central.Models.BasicInfo;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Sales
{
    public class SellModel : IModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string QRCode { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

        [JsonProperty("avl")]
        public decimal Available { get; set; }

        [JsonProperty("rev")]
        public decimal Reserve { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("price")]
        public PriceModel Price { get; set; }
    }
}
