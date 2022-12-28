using System.Net.Sockets;

namespace RPC_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rpcClient = new RPCClient();

            Console.WriteLine(" [x] Requesting fib(30)");
            var response = rpcClient.Call("30");

            Console.WriteLine(" [.] Got '{0}'", response);
            rpcClient.Close();
        }
    }
}