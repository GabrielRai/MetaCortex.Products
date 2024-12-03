using MongoDB.Bson;
using System.ComponentModel;

namespace MetaCortex.Products.DataAccess
{
    public class Product
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
