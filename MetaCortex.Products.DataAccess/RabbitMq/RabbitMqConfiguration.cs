using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCortex.Products.DataAccess.RabbitMq
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; init; } = "localhost";
        public string UserName { get; init; } = "guest";
        public string Password { get; init; } = "guest";
    }
}
