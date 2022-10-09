namespace Catalog.Api.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catalog.Api.Entities;

    public interface IProductRepository
    {

        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);

        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
    }
}