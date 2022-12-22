
using RabbitMQ.Client;
using System.Text;

namespace Send;
class Program
{
    private static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    }

    static void Main(string[] args)
    {
        var message = "";

        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {

            //setando a Queu para durar mais tempo mesmo após uma request
            channel.QueueDeclare(queue: "task_queue",
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

            message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);

            //Setando a msg para nao expirar 
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                 routingKey: "task_queue",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine(" [x] Sent {0}", message);
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
