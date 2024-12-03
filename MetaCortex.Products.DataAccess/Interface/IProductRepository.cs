using MetaCortex.Products.DataAccess.Class;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCortex.Products.DataAccess.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(ObjectId id);
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(ObjectId id, Product product);
        Task DeleteProduct(ObjectId id);
    }
}
