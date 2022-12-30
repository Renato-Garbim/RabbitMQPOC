using RabbitMQRPC_Server_POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQRPC_Server_POC.Service
{
    public class OrderService
    {
        public static OrderStatus OnStore(decimal amount)
        {
            return (amount < 0 || amount > 1000) ? OrderStatus.Declined : OrderStatus.Aproved;
        }
    }
}
