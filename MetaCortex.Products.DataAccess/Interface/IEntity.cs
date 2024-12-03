using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCortex.Products.DataAccess.Interface
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
