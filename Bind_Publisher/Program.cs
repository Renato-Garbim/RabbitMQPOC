using RabbitMQ.Client;
using System.Text;

namespace Bind_Publisher
{
    internal class Program
    {
        static void Main(string[] msg)
        {
            msg = new[] { "error",  "Run", "Run", "Or", "it", "will", "explode." };

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");

                var severity = (msg.Length > 0) ? msg[0] : "info";

                var message = (msg.Length > 1)
                              ? string.Join(" ", msg.Skip(1).ToArray())
                              : "Hello World!";


                var body = Encoding.UTF8.GetBytes(message);

                //Setando a msg para nao expirar                 

                channel.BasicPublish(exchange: "direct_logs",
                     routingKey: severity,
                     basicProperties: null,
                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}