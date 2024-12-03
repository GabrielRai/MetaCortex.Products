using MongoDB.Bson;
using System.ComponentModel;

namespace MetaCortex.Products.DataAccess.Class
{
    public class Product : EntityBase
    {
        public ObjectId ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
