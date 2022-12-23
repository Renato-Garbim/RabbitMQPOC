using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Exchange_Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            IModel? _channel = ConnectionBuilder.MontarChannel("localhost");
            ConnectionBuilder.ExchangeConfig(_channel, "direct_logs", "direct");
            string _queuName = ConnectionBuilder.CriarQueuGenericaDescartavel(_channel);
            
            using (_channel)
            {                                
                ConnectionBuilder.GerarBindingParaCadaStatusDoSistema(_channel, _queuName, "direct_logs", new List<string>() { "info", "warning", "error" });

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine(" [x] Received '{0}':'{1}'",
                                      routingKey, message);
                };

                _channel.BasicConsume(queue: _queuName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
