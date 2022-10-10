namespace Catalog.Api.Repositories
{
    using System;
    using System.Linq;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catalog.Api.Entities;
    using Catalog.Api.Data;

    public class ProductRepository: IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateProduct(Product product)
        {
            try
            {
                await _context
                    .Products
                    .InsertOneAsync(product);
                return true;
            } catch(Exception)
            {   
                return false;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                .Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(prod => prod.Id, id);

            var deleteOperation = await _context
                .Products
                .DeleteOneAsync(filter);

            return deleteOperation.IsAcknowledged && deleteOperation.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                .Products
                .Find(prod => prod.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                .Products
                .Find(prod => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(prod => prod.Name, name);
            return await _context
                .Products
                .Find<Product>(filter)
                .ToListAsync<Product>();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(prod => prod.Category, category);
            return await _context
                .Products
                .Find<Product>(filter)
                .ToListAsync<Product>();
        }
    }
}