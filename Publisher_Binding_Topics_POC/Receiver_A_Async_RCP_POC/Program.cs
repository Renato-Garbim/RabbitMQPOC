
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Receiver_A_Async_RCP_POC
{
    public class Program
    {
               
        static void Main(string[] args)
        {
            RpcClient _rpcClient = new RpcClient();

            _rpcClient.InitializeAndRun();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

    }
}