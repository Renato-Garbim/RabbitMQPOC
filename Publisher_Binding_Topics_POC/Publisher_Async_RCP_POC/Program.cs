using Publisher_Async_RPC_POC;
using Publisher_Async_RPC_POC.Events;
using RabbitMQ.Client;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Publisher_Async_RCP_POC
{
    public class Program
    {
        static async void Main(string[] args)
        {

            var _rpcClient = new RpcClient();
            _rpcClient.Initialiaze();

            var task = await ExecuteFetchCommand();

            //var FetchCommand = ExecuteSendAsync<string> ExecuteFetchCommand;

            Console.WriteLine("Hello, World!");
        }

        public async Task ExecuteFetchCommand()
        {
            var _rpcClient = new RpcClient();

            var response = await _rpcClient.SendAsync("15");

            //LogMessages.Add($"Generating 15 UserNames");

            //await foreach (var userName in DeserializeStreaming<string>(response))
            //{
            //    LogMessages.Add(userName);
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}