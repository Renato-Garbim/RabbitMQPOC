using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQRPC_Client_POC.Models
{
    public sealed class Order
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string Status => OrderStatus.ToString();
        private OrderStatus OrderStatus { get; set; }

        public Order(decimal amount)
        {
            Id = DateTime.Now.Ticks;
            OrderStatus = OrderStatus.Processing;
            Amount = amount;
        }
    }
}
