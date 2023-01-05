using Publisher_Async_RPC_POC;
using Publisher_Async_RPC_POC.Events;
using RabbitMQ.Client;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Publisher_Async_RCP_POC
{
    public class Program
    {
         static void Main(string[] args)
         {

            MainAsync().Wait();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

        static async Task MainAsync()
        {
            var _rpcClient = new RpcClient();
            _rpcClient.Initialiaze();

            var response = await _rpcClient.SendAsync("15");

            Console.WriteLine(" [.] Got '{0}'", response);

            response = await _rpcClient.SendAsync("20");

            Console.WriteLine(" [.] Got '{0}'", response);
        }

        
       
    }
}