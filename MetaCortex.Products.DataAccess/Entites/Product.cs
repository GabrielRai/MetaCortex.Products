using MongoDB.Bson;
using System.ComponentModel;

namespace MetaCortex.Products.DataAccess.Class
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int OrderStock { get; set; }
    }
}
