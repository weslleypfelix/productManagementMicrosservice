using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetProducts();
        Task<ProductEntity> GetProduct(string id);
        Task<IEnumerable<ProductEntity>> GetProductByName(string name);
        Task<IEnumerable<ProductEntity>> GetProductByCategory(string categoryName);
        Task CreateProduct(ProductEntity productEntity);
        Task<bool> UpdateProduct(ProductEntity productEntity);
        Task<bool> DeleteProduct(string id);
    }
}
