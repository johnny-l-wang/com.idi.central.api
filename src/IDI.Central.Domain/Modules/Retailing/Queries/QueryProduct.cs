﻿using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryProductCondition : Condition { }

    public class QueryProduct : Query<QueryProductCondition, Collection<ProductModel>>
    {
        [Injection]
        public IQueryRepository<AggregateRoots.Product> Products { get; set; }

        public override Result<Collection<ProductModel>> Execute(QueryProductCondition condition)
        {
            var products = this.Products.Get();

            var collection = products.OrderBy(r => r.Name).Select(r => new ProductModel
            {
                Id = r.Id,
                Name = r.Name,
                Code = r.Code,
                Description = r.Tags.To<List<Tag>>().AsString(),
                Tags = r.Tags.To<List<Tag>>(),
                Enabled = r.Enabled
            }).ToList();

            return Result.Success(new Collection<ProductModel>(collection));
        }
    }
}
