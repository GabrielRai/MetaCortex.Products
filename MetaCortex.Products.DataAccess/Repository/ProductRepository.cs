using MetaCortex.Products.DataAccess.Class;
using MetaCortex.Products.DataAccess.Interface;
using MetaCortex.Products.DataAccess.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCortex.Products.DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMongoCollection<Product> _products;
        public ProductRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
        {
            var settings = mongoDbSettings.Value;
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>(mongoDbSettings.Value.CollectionName, new MongoCollectionSettings { AssignIdOnInsert = true });
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _products.Find(p => true).ToListAsync();
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }
        public async Task UpdateProduct(string id, Product product)
        {
            await _products.ReplaceOneAsync(p => p.Id == id, product);
        }
        public async Task DeleteProduct(string id)
        {
            await _products.DeleteOneAsync(p => p.Id == id);
        }

        public async Task UpdateProductOrderStock(string name, int quantity)
        {
            await _products.UpdateOneAsync(p => p.Name == name, Builders<Product>.Update.Inc(p => p.OrderStock, quantity));
        }
    }
}
