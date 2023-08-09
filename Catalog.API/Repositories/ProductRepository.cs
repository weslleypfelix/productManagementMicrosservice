using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task CreateProduct(ProductEntity productEntity)
        {
            await _catalogContext.Products.InsertOneAsync(productEntity);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<ProductEntity> filter = Builders<ProductEntity>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount> 0;
        }

        public async Task<ProductEntity> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProductEntity> GetProductById(string id)
        {
            FilterDefinition<ProductEntity> filter = Builders<ProductEntity>.Filter.Eq(p => p.Id, id);

            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<ProductEntity>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<ProductEntity> filter = Builders<ProductEntity>.Filter.Eq(p => p.Category, categoryName);

            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetProductByName(string name)
        {
            FilterDefinition<ProductEntity> filter = Builders<ProductEntity>.Filter.Eq(p => p.Name, name);

            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            return await _catalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(ProductEntity productEntity)
        {
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(
                filter: g => g.Id == productEntity.Id, replacement: productEntity);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
