﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCortex.Products.DataAccess.RabbitMq
{
    public interface IMessageConsumerService
    {
        //Task ReadMessagesAsync(string queueName, Func<string, Task>);
        Task ReadFinalOrderAsync(string queue);
    }
}
