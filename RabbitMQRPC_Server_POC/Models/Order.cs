using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQRPC_Server_POC.Models
{
    public class Order
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string Status => OrderStatus.ToString();
        private OrderStatus OrderStatus { get; set; }

        public void SetStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }
    }
}
