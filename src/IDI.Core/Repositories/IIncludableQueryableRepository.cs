﻿using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore.Query;

namespace IDI.Core.Repositories
{
    public interface IIncludableQueryableRepository<TAggregateRoot, TProperty> : IQueryableRepository<TAggregateRoot>, IIncludableQueryable<TAggregateRoot, TProperty> where TAggregateRoot : AggregateRoot { }
}
