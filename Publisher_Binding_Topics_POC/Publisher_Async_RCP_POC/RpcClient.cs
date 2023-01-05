using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher_Async_RPC_POC
{
    public class RpcClient
    {
        private IConnection _connection;
        private IModel _channel;
        private string _responseQueueName;
        private string QueueName = "UserRpcQueue";
        private bool _isDisposed;

        private ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingMessages = new ConcurrentDictionary<string, TaskCompletionSource<string>>();

        public void Initialiaze()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _responseQueueName = _channel.QueueDeclare().QueueName;

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += Consumer_Received;
            _channel.BasicConsume(queue: _responseQueueName, consumer: consumer, autoAck: true);
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            if (_pendingMessages.TryRemove(args.BasicProperties.CorrelationId, out var taskCompletionSource))
            {
                taskCompletionSource.SetResult(message);
            }
        }

        private void Publish(string message, string correlationId)
        {
            var props = _channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = _responseQueueName;

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "UserRpcQueue", props, body: messageBytes);

            Console.WriteLine($"Sent: {message} with CorrelationId {correlationId}");
        }

        public Task<string> SendAsync(string message)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();

            var messageId = Guid.NewGuid().ToString();
            
            Publish(message, messageId);

            //_pendingMessages[messageId] = taskCompletionSource;

            _pendingMessages.TryAdd(messageId, taskCompletionSource);
            return taskCompletionSource.Task;
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
            {
                _channel.Close();
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RpcClient()
        {
            Dispose(false);
        }

    }
}
