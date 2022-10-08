using System;
namespace Catalog.Api.Data
{
    using MongoDB.Driver;
    using Catalog.Api.Entities;

    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}

