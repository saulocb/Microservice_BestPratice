﻿using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
