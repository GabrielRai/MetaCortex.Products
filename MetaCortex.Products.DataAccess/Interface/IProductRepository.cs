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
        Task<Product> GetProduct(string id);
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(string id, Product product);
        Task DeleteProduct(string id);
        Task UpdateProductOrderStock(string name, int quantity);
    }
}
