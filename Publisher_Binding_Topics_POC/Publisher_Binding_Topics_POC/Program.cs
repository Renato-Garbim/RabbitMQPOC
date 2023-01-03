using RabbitMQ.Client;
using System.Text;

namespace Publisher_Binding_Topics_POC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rpcClient = new RpcClient();

            //var message = "Message teste chegar em todos os Receivers";

            Console.WriteLine(" [x] Requesting fib(30)");

            var response = rpcClient.Call("30");


            Console.WriteLine(" [.] Got '{0}'", response);

            rpcClient.Close();
        }

       
    }
}