using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<ProductEntity> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetMyProducts());
            }
        }

        private static IEnumerable<ProductEntity> GetMyProducts()
        {
            return new List<ProductEntity>()
            {
                new ProductEntity()
                {
                    Id = "asdadad",
                    Name= "Name",
                    Description= "Description",
                    Image = "product.png",
                    Price = 900,
                    Category= "Category"
                },
                new ProductEntity()
                {
                    Id = "vddvdfvdffvd",
                    Name= "Name2",
                    Description= "Description2",
                    Image = "producat.png",
                    Price = 9100,
                    Category= "Category2"
                }
            };
        }


    }
}
