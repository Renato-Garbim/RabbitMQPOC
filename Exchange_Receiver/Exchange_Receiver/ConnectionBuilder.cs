using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Exchange_Receiver
{
    public static class ConnectionBuilder
    {
        public static IModel? MontarChannel(string hostname)
        {
            //"localhost"
            var factory = new ConnectionFactory() { HostName = hostname };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            return channel;
        }

        public static void ExchangeConfig(IModel channel, string exchangeName, string exchangeType)
        {
            //"direct_logs"
            //direct
            channel.ExchangeDeclare(exchange: exchangeName , type: exchangeType);
        }

        public static string CriarQueuGenericaDescartavel(IModel channel)
        {
            return channel.QueueDeclare().QueueName;
        }

        public static void GerarBindingParaCadaStatusDoSistema(IModel channel,string queuName, string exchangeName, IList<string> status)
        {
            foreach(var severity in status)
            {
                channel.QueueBind(queue: queuName,
                  exchange: exchangeName,
                  routingKey: severity);
            }
        }

        

    }
}
