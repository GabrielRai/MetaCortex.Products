using MetaCortex.Products.DataAccess.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCortex.Products.DataAccess.Class
{
    public class EntityBase : IEntity<ObjectId>
    {
        public ObjectId Id { get; set; }
    }
}
